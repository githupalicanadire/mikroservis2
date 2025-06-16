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
            try
            {
                var userIdentifier = userService.GetSecureUserIdentifier();

                logger.LogInformation("Add to cart from product detail for user: {UserId}, product: {ProductId}", userIdentifier, productId);

                var productResponse = await catalogService.GetProduct(productId);
                if (productResponse?.Product == null)
                {
                    logger.LogWarning("Product {ProductId} not found", productId);
                    return RedirectToPage();
                }

                var basket = await basketService.LoadUserBasket(userIdentifier);

                // Security check: ensure loaded basket belongs to current user
                if (!string.Equals(basket.UserName, userIdentifier, StringComparison.OrdinalIgnoreCase))
                {
                    logger.LogWarning("User {UserId} got basket that doesn't belong to them", userIdentifier);
                    // Create new basket for user
                    basket = new ShoppingCartModel { UserName = userIdentifier, Items = new List<ShoppingCartItemModel>() };
                }

                // Check if item already exists in cart
                var existingItem = basket.Items.FirstOrDefault(x => x.ProductId == productId && x.Color == Color);
                if (existingItem != null)
                {
                    // Update quantity if item exists
                    existingItem.Quantity += Quantity;
                    logger.LogInformation("Updated existing item quantity in cart for user: {UserId}", userIdentifier);
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
                    logger.LogInformation("Added new item to cart for user: {UserId}", userIdentifier);
                }

                await basketService.StoreBasket(new StoreBasketRequest(basket));

                TempData["SuccessMessage"] = $"'{productResponse.Product.Name}' has been added to your cart!";
                return RedirectToPage("Cart");
            }
            catch (UnauthorizedAccessException)
            {
                // Store intended action and redirect to login
                TempData["ReturnUrl"] = $"/ProductDetail?productId={productId}";
                TempData["Message"] = "Please login to add items to your cart.";
                return RedirectToPage("/Login");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding product {ProductId} to cart", productId);
                TempData["ErrorMessage"] = "An error occurred while adding the item to your cart.";
                return RedirectToPage();
            }
        }
    }
}
