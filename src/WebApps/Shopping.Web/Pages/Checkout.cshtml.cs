using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Web.Models.Basket;
using Shopping.Web.Services;

namespace Shopping.Web.Pages
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class CheckoutModel
        (IBasketService basketService, IUserService userService, ILogger<CheckoutModel> logger)
        : PageModel
    {
        [BindProperty]
        public BasketCheckoutModel Order { get; set; } = default!;

        public ShoppingCartModel Cart { get; set; } = new ShoppingCartModel();

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var userIdentifier = userService.GetSecureUserIdentifier();

                Cart = await basketService.LoadUserBasket(userIdentifier);

                // Verify cart belongs to current user
                if (!string.Equals(Cart.UserName, userIdentifier, StringComparison.OrdinalIgnoreCase))
                {
                    logger.LogWarning("User {UserId} attempted to checkout cart that doesn't belong to them", userIdentifier);
                    return RedirectToPage("/Login");
                }

                // Ensure cart has items
                if (!Cart.Items.Any())
                {
                    logger.LogInformation("User {UserId} attempted to checkout with empty cart", userIdentifier);
                    return RedirectToPage("/Cart");
                }

                return Page();
            }
            catch (UnauthorizedAccessException)
            {
                logger.LogWarning("Unauthorized access attempt to checkout");
                return RedirectToPage("/Login");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading checkout page");
                return RedirectToPage("/Cart");
            }
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

                Order.UserName = userIdentifier;
                Order.CustomerId = customerGuid;
                Order.TotalPrice = Cart.TotalPrice;

                var basketCheckoutDto = new BasketCheckoutModel
                {
                    UserName = userIdentifier,
                    CustomerId = customerGuid,
                    TotalPrice = Cart.TotalPrice,
                    FirstName = Order.FirstName,
                    LastName = Order.LastName,
                    EmailAddress = Order.EmailAddress ?? userService.GetCurrentUserEmail() ?? "",
                    AddressLine = Order.AddressLine,
                    Country = Order.Country,
                    State = Order.State,
                    ZipCode = Order.ZipCode,
                    CardName = Order.CardName,
                    CardNumber = Order.CardNumber,
                    Expiration = Order.Expiration,
                    CVV = Order.CVV,
                    PaymentMethod = Order.PaymentMethod
                };

                var checkoutBasketRequest = new CheckoutBasketRequest(basketCheckoutDto);

                await basketService.CheckoutBasket(checkoutBasketRequest);
                logger.LogInformation("Checkout completed successfully for user: {UserId}", userIdentifier);

                return RedirectToPage("Confirmation", "OrderSubmitted");
            }
            catch (UnauthorizedAccessException)
            {
                logger.LogWarning("Unauthorized checkout attempt");
                return RedirectToPage("/Login");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during checkout for user");
                ModelState.AddModelError("", "An error occurred during checkout. Please try again.");

                // Reload cart for display
                try
                {
                    var userIdentifier = userService.GetSecureUserIdentifier();
                    Cart = await basketService.LoadUserBasket(userIdentifier);
                }
                catch
                {
                    Cart = new ShoppingCartModel();
                }

                return Page();
            }
        }
    }
}
