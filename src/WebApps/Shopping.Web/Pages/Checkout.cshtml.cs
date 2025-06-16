namespace Shopping.Web.Pages
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class CheckoutModel
        (IBasketService basketService, IUserService userService, ILogger<CheckoutModel> logger)
        : PageModel
    {
        [BindProperty]
        public BasketCheckoutModel Order { get; set; } = default!;
        public ShoppingCartModel Cart { get; set; } = default!;

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
                return RedirectToPage("/Login");
            }

            Cart = await basketService.LoadUserBasket(userName);
            logger.LogInformation("Checkout page loaded for user: {UserName}", userName);

            return Page();
        }

        public async Task<IActionResult> OnPostCheckOutAsync()
        {
            // Check if user is authenticated
            if (!userService.IsAuthenticated())
            {
                return RedirectToPage("/Login");
            }

            var userName = userService.GetCurrentUserName() ?? userService.GetCurrentUserEmail();
            var userId = userService.GetCurrentUserId();

            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToPage("/Login");
            }

            logger.LogInformation("Checkout button clicked for user: {UserName}", userName);

            Cart = await basketService.LoadUserBasket(userName);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Use authenticated user's information
            if (string.IsNullOrEmpty(userId))
            {
                // Generate deterministic GUID from username for demo
                using var md5 = System.Security.Cryptography.MD5.Create();
                var hash = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userName));
                Order.CustomerId = new Guid(hash);
            }
            else
            {
                Order.CustomerId = Guid.Parse(userId);
            }

            Order.UserName = userName;
            Order.TotalPrice = Cart.TotalPrice;

            logger.LogInformation("Processing checkout for user: {UserName}, amount: {TotalPrice}", userName, Order.TotalPrice);

            await basketService.CheckoutBasket(new CheckoutBasketRequest(Order));

            return RedirectToPage("Confirmation", "OrderSubmitted");
        }
    }
}
