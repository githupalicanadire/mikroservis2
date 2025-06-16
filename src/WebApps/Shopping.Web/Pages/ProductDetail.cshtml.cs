namespace Shopping.Web.Pages
{
    public class ProductDetailModel
        (ICatalogService catalogService, IBasketService basketService, IUserService userService, ILogger<ProductDetailModel> logger)
        : PageModel
    {
        public ProductModel Product { get; set; } = default!;

        [BindProperty]
        public string Color { get; set; } = default!;

        [BindProperty]
        public int Quantity { get; set; } = 1;

        public async Task<IActionResult> OnGetAsync(Guid productId)
        {
            var response = await catalogService.GetProduct(productId);
            Product = response.Product;

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
        {
            // Check if user is authenticated for cart operations
            if (!userService.IsAuthenticated())
            {
                // Store intended action and redirect to login
                TempData["ReturnUrl"] = $"/ProductDetail?productId={productId}";
                TempData["Message"] = "Please login to add items to your cart.";
                return RedirectToPage("/Login");
            }

            var userName = userService.GetCurrentUserName() ?? userService.GetCurrentUserEmail();
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToPage("/Login");
            }

            logger.LogInformation("Add to cart button clicked by user: {UserName} for product: {ProductId}", userName, productId);

            var productResponse = await catalogService.GetProduct(productId);
            var basket = await basketService.LoadUserBasket(userName);

            // Check if item already exists in cart
            var existingItem = basket.Items.FirstOrDefault(x => x.ProductId == productId && x.Color == Color);
            if (existingItem != null)
            {
                // Update quantity if item exists
                existingItem.Quantity += Quantity;
                logger.LogInformation("Updated existing item quantity in cart for user: {UserName}", userName);
            }
            else
            {
                // Add new item to cart
                basket.Items.Add(new ShoppingCartItemModel
                {
                    ProductId = productId,
                    ProductName = productResponse.Product.Name,
                    Price = productResponse.Product.Price,
                    Quantity = Quantity,
                    Color = Color
                });
                logger.LogInformation("Added new item to cart for user: {UserName}", userName);
            }

            await basketService.StoreBasket(new StoreBasketRequest(basket));

            TempData["SuccessMessage"] = $"'{productResponse.Product.Name}' has been added to your cart!";
            return RedirectToPage("Cart");
        }
    }
}
