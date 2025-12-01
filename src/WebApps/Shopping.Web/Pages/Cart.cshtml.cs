using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Web.Models.Basket;
using Shopping.Web.Services;

namespace Shopping.Web.Pages
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class CartModel(IBasketService basketService, IUserService userService, ILogger<CartModel> logger)
        : PageModel
    {
        public ShoppingCartModel Cart { get; set; } = new ShoppingCartModel();

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                // Use secure user identifier instead of username/email
                var userIdentifier = userService.GetSecureUserIdentifier();

                Cart = await basketService.LoadUserBasket(userIdentifier);
                logger.LogInformation("Cart loaded for user: {UserId}", userIdentifier);

                return Page();
            }
            catch (UnauthorizedAccessException)
            {
                logger.LogWarning("Unauthorized access attempt to cart");
                return RedirectToPage("/Login");
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Could not determine user identity for cart");
                return RedirectToPage("/Login");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading cart");
                return RedirectToPage("/Index");
            }
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(Guid productId)
        {
            try
            {
                var userIdentifier = userService.GetSecureUserIdentifier();

                logger.LogInformation("Remove from cart for user: {UserId}, product: {ProductId}", userIdentifier, productId);

                Cart = await basketService.LoadUserBasket(userIdentifier);

                // Verify cart belongs to current user
                if (!string.Equals(Cart.UserName, userIdentifier, StringComparison.OrdinalIgnoreCase))
                {
                    logger.LogWarning("User {UserId} attempted to modify cart that doesn't belong to them", userIdentifier);
                    return RedirectToPage("/Login");
                }

                Cart.Items.RemoveAll(x => x.ProductId == productId);
                await basketService.StoreBasket(new StoreBasketRequest(Cart));

                logger.LogInformation("Item removed from cart for user: {UserId}", userIdentifier);

                return RedirectToPage();
            }
            catch (UnauthorizedAccessException)
            {
                logger.LogWarning("Unauthorized cart modification attempt");
                return RedirectToPage("/Login");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error removing item from cart");
                return RedirectToPage();
            }
        }
    }
}
