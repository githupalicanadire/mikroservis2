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
            try
            {
                var userIdentifier = userService.GetSecureUserIdentifier();
                var userName = userService.GetCurrentUserName() ?? userService.GetCurrentUserEmail();

                logger.LogInformation("Checkout initiated for user: {UserId}", userIdentifier);

                Cart = await basketService.LoadUserBasket(userIdentifier);

                // Security validation: ensure cart belongs to current user
                if (!string.Equals(Cart.UserName, userIdentifier, StringComparison.OrdinalIgnoreCase))
                {
                    logger.LogWarning("User {UserId} attempted to checkout cart that doesn't belong to them", userIdentifier);
                    return RedirectToPage("/Login");
                }

                if (!Cart.Items.Any())
                {
                    logger.LogWarning("User {UserId} attempted to checkout with empty cart", userIdentifier);
                    return RedirectToPage("/Cart");
                }

                if (!ModelState.IsValid)
                {
                    return Page();
                }

                // Parse user identifier as customer GUID
                if (!Guid.TryParse(userIdentifier, out var customerGuid))
                {
                    logger.LogError("Invalid user identifier format for checkout: {UserId}", userIdentifier);
                    return RedirectToPage("/Login");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading checkout page");
                return RedirectToPage("/Cart");
            }
        }

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
