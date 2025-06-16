using System.Net;

namespace Shopping.Web.Services;

public interface IBasketService
{
    [Get("/basket-service/basket/{userName}")]
    Task<GetBasketResponse> GetBasket(string userName);

    [Post("/basket-service/basket")]
    Task<StoreBasketResponse> StoreBasket(StoreBasketRequest request);

    [Delete("/basket-service/basket/{userName}")]
    Task<DeleteBasketResponse> DeleteBasket(string userName);

    [Post("/basket-service/basket/checkout")]
    Task<CheckoutBasketResponse> CheckoutBasket(CheckoutBasketRequest request);

    public async Task<ShoppingCartModel> LoadUserBasket(string userIdentifier)
    {
        if (string.IsNullOrEmpty(userIdentifier))
        {
            throw new ArgumentException("User identifier cannot be null or empty", nameof(userIdentifier));
        }

        ShoppingCartModel basket;

        try
        {
            var getBasketResponse = await GetBasket(userIdentifier);
            basket = getBasketResponse.Cart;

            // Security validation: ensure returned basket belongs to the user
            if (!string.Equals(basket.UserName, userIdentifier, StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException($"Basket does not belong to user {userIdentifier}");
            }
        }
        catch (ApiException apiException) when (apiException.StatusCode == HttpStatusCode.NotFound)
        {
            // Create new basket for user if not found
            basket = new ShoppingCartModel
            {
                UserName = userIdentifier,
                Items = new List<ShoppingCartItemModel>()
            };
        }

        return basket;
    }
}
