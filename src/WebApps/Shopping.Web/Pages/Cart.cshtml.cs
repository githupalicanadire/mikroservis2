namespace Shopping.Web.Pages
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class CartModel(IBasketService basketService, IUserService userService, ILogger<CartModel> logger)
        : PageModel
    {
        public ShoppingCartModel Cart { get; set; } = new ShoppingCartModel();

        public async Task<IActionResult> OnGetAsync()
        {
            // Check if user is authenticated
            if (!userService.IsAuthenticated())
            {
                return RedirectToPage("/Login");
            }

            var userName = userService.GetCurrentUserName() ?? userService.GetCurrentUserEmail();
            if (string.IsNullOrEmpty(userName))
            {
                logger.LogWarning("Could not determine user name for cart");
                return RedirectToPage("/Login");
            }

            Cart = await basketService.LoadUserBasket(userName);
            logger.LogInformation("Cart loaded for user: {UserName}", userName);

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(Guid productId)
        {
            // Check if user is authenticated
            if (!userService.IsAuthenticated())
            {
                return RedirectToPage("/Login");
            }

            var userName = userService.GetCurrentUserName() ?? userService.GetCurrentUserEmail();
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToPage("/Login");
            }

            logger.LogInformation("Remove from cart button clicked for user: {UserName}, product: {ProductId}", userName, productId);

            Cart = await basketService.LoadUserBasket(userName);
            Cart.Items.RemoveAll(x => x.ProductId == productId);

            await basketService.StoreBasket(new StoreBasketRequest(Cart));
            logger.LogInformation("Item removed from cart for user: {UserName}", userName);

            return RedirectToPage();
        }
    }
}
