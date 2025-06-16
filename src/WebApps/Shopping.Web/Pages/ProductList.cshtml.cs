namespace Shopping.Web.Pages
{
    public class ProductListModel
        (ICatalogService catalogService, IBasketService basketService, ILogger<ProductListModel> logger)
        : PageModel
    {
        public IEnumerable<string> CategoryList { get; set; } = [];
        public IEnumerable<ProductModel> ProductList { get; set; } = [];
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 12;
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public new int Page { get; set; } = 1;

        public async Task<IActionResult> OnGetAsync(string categoryName)
        {
            try
            {
                logger.LogInformation("Loading products for page {Page} with category {Category}", Page, categoryName);

                CurrentPage = Page;
                var response = await catalogService.GetAllProducts();

                if (response?.Products == null)
                {
                    logger.LogWarning("No products returned from catalog service");
                    return Page();
                }

                // Get all categories from the catalog service
                CategoryList = response.Products
                    .SelectMany(p => p.Category)
                    .Distinct()
                    .OrderBy(c => c)
                    .ToList();

                // Filter products by category if selected
                var filteredProducts = !string.IsNullOrWhiteSpace(categoryName)
                    ? response.Products.Where(p => p.Category.Contains(categoryName))
                    : response.Products;

                SelectedCategory = categoryName;

                // Calculate pagination
                TotalItems = filteredProducts.Count();
                TotalPages = (int)Math.Ceiling(TotalItems / (double)PageSize);

                // Ensure current page is valid
                if (CurrentPage < 1) CurrentPage = 1;
                if (CurrentPage > TotalPages && TotalPages > 0) CurrentPage = TotalPages;

                // Apply pagination on the client side
                var products = filteredProducts.ToList();
                ProductList = products
                    .Skip((CurrentPage - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();

                logger.LogInformation(
                    "Successfully loaded {Count} products for page {Page} of {TotalPages} (Total items: {TotalItems})", 
                    ProductList.Count(), 
                    CurrentPage,
                    TotalPages,
                    TotalItems);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while loading products");
                // You might want to show an error message to the user here
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
        {
            try
            {
                logger.LogInformation("Add to cart button clicked for product {ProductId}", productId);

                var productResponse = await catalogService.GetProduct(productId);

                if (productResponse?.Product == null)
                {
                    logger.LogWarning("Product {ProductId} not found", productId);
                    return RedirectToPage("ProductList");
                }

                var basket = await basketService.LoadUserBasket();

                basket.Items.Add(new ShoppingCartItemModel
                {
                    ProductId = productId,
                    ProductName = productResponse.Product.Name,
                    Price = productResponse.Product.Price,
                    Quantity = 1,
                    Color = "Black"
                });

                await basketService.StoreBasket(new StoreBasketRequest(basket));
                logger.LogInformation("Product {ProductId} added to cart successfully", productId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while adding product {ProductId} to cart", productId);
            }

            return RedirectToPage("Cart");
        }
    }
}
