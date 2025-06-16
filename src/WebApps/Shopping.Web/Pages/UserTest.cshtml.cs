using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Web.Models.Catalog;
using Shopping.Web.Services;
using System.Text;

namespace Shopping.Web.Pages;

[Microsoft.AspNetCore.Authorization.Authorize]
public class UserTestModel : PageModel
{
    private readonly IUserService _userService;
    private readonly IBasketService _basketService;
    private readonly IOrderingService _orderingService;
    private readonly ICatalogService _catalogService;
    private readonly ILogger<UserTestModel> _logger;

    public UserTestModel(
        IUserService userService,
        IBasketService basketService,
        IOrderingService orderingService,
        ICatalogService catalogService,
        ILogger<UserTestModel> logger)
    {
        _userService = userService;
        _basketService = basketService;
        _orderingService = orderingService;
        _catalogService = catalogService;
        _logger = logger;
    }

    public string CurrentUserId { get; set; } = default!;
    public string? CurrentUserEmail { get; set; }
    public string? CurrentUserName { get; set; }
    public int CartItemCount { get; set; }
    public int OrderCount { get; set; }
    public bool CartSecurityTest { get; set; }
    public bool OrderSecurityTest { get; set; }
    public bool UserIdentityTest { get; set; }
    public string? TestResults { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            await LoadUserData();
            await RunSecurityValidation();
            return Page();
        }
        catch (UnauthorizedAccessException)
        {
            return RedirectToPage("/Login");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading user test page");
            return RedirectToPage("/Index");
        }
    }

    public async Task<IActionResult> OnPostAddTestProductAsync()
    {
        try
        {
            var userIdentifier = _userService.GetSecureUserIdentifier();

            // Get first available product
            var productsResponse = await _catalogService.GetProducts(1, 1);
            if (productsResponse?.Products?.Any() == true)
            {
                var product = productsResponse.Products.First();
                var basket = await _basketService.LoadUserBasket(userIdentifier);

                basket.Items.Add(new Shopping.Web.Models.Basket.ShoppingCartItemModel
                {
                    ProductId = product.Id,
                    ProductName = $"Test Product - {DateTime.Now:HH:mm:ss}",
                    Price = 9.99m,
                    Quantity = 1,
                    Color = "Test"
                });

                await _basketService.StoreBasket(new Shopping.Web.Models.Basket.StoreBasketRequest(basket));

                TestResults = $"✅ Test product added to cart at {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n" +
                             $"User ID: {userIdentifier}\n" +
                             $"Product: {product.Name}\n" +
                             $"Cart Items: {basket.Items.Count}";
            }

            await LoadUserData();
            await RunSecurityValidation();
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding test product");
            TestResults = $"❌ Error adding test product: {ex.Message}";
            return Page();
        }
    }

    public async Task<IActionResult> OnPostCreateTestOrderAsync()
    {
        try
        {
            var userIdentifier = _userService.GetSecureUserIdentifier();
            var basket = await _basketService.LoadUserBasket(userIdentifier);

            if (!basket.Items.Any())
            {
                TestResults = "❌ Cannot create order: Cart is empty. Add test product first.";
                await LoadUserData();
                return Page();
            }

            // Create test order through checkout
            var basketCheckoutDto = new Shopping.Web.Models.Basket.BasketCheckoutModel
            {
                UserName = userIdentifier,
                CustomerId = Guid.Parse(userIdentifier),
                TotalPrice = basket.TotalPrice,
                FirstName = "Test",
                LastName = "User",
                EmailAddress = _userService.GetCurrentUserEmail() ?? "test@example.com",
                AddressLine = "Test Address",
                Country = "Test Country",
                State = "Test State",
                ZipCode = "12345",
                CardName = "Test Card",
                CardNumber = "4111111111111111",
                Expiration = "12/25",
                CVV = "123",
                PaymentMethod = 1
            };

            var checkoutRequest = new Shopping.Web.Models.Basket.CheckoutBasketRequest(basketCheckoutDto);

            await _basketService.CheckoutBasket(checkoutRequest);

            TestResults = $"✅ Test order created at {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n" +
                         $"User ID: {userIdentifier}\n" +
                         $"Items: {basket.Items.Count}\n" +
                         $"Total: ${basket.TotalPrice:F2}";

            await LoadUserData();
            await RunSecurityValidation();
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating test order");
            TestResults = $"❌ Error creating test order: {ex.Message}";
            return Page();
        }
    }

    public async Task<IActionResult> OnPostRunSecurityTestsAsync()
    {
        try
        {
            var results = new StringBuilder();
            results.AppendLine("🛡️ SECURITY TEST RESULTS");
            results.AppendLine($"Timestamp: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            results.AppendLine();

            var userIdentifier = _userService.GetSecureUserIdentifier();
            results.AppendLine($"Current User ID: {userIdentifier}");
            results.AppendLine();

            // Test 1: User Identity Security
            results.AppendLine("TEST 1: User Identity Security");
            try
            {
                var userId = _userService.GetCurrentUserId();
                var email = _userService.GetCurrentUserEmail();
                var name = _userService.GetCurrentUserName();

                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(email))
                {
                    results.AppendLine("✅ User identity retrieval successful");
                    UserIdentityTest = true;
                }
                else
                {
                    results.AppendLine("❌ User identity retrieval failed");
                    UserIdentityTest = false;
                }
            }
            catch (Exception ex)
            {
                results.AppendLine($"❌ User identity test failed: {ex.Message}");
                UserIdentityTest = false;
            }
            results.AppendLine();

            // Test 2: Cart Isolation
            results.AppendLine("TEST 2: Cart Isolation");
            try
            {
                var basket = await _basketService.LoadUserBasket(userIdentifier);

                if (string.Equals(basket.UserName, userIdentifier, StringComparison.OrdinalIgnoreCase))
                {
                    results.AppendLine("✅ Cart belongs to current user");
                    results.AppendLine($"   Cart Owner: {basket.UserName}");
                    results.AppendLine($"   Current User: {userIdentifier}");
                    results.AppendLine($"   Items Count: {basket.Items.Count}");
                    CartSecurityTest = true;
                }
                else
                {
                    results.AppendLine("❌ Cart ownership mismatch!");
                    results.AppendLine($"   Cart Owner: {basket.UserName}");
                    results.AppendLine($"   Current User: {userIdentifier}");
                    CartSecurityTest = false;
                }
            }
            catch (Exception ex)
            {
                results.AppendLine($"❌ Cart isolation test failed: {ex.Message}");
                CartSecurityTest = false;
            }
            results.AppendLine();

            // Test 3: Order Isolation
            results.AppendLine("TEST 3: Order Isolation");
            try
            {
                if (Guid.TryParse(userIdentifier, out var customerGuid))
                {
                    var ordersResponse = await _orderingService.GetOrdersByCustomer(customerGuid);

                    if (ordersResponse?.Orders != null)
                    {
                        var userOrders = ordersResponse.Orders.Where(o => o.CustomerId == customerGuid).ToList();

                        if (userOrders.Count == ordersResponse.Orders.Count())
                        {
                            results.AppendLine("✅ All orders belong to current user");
                            results.AppendLine($"   Orders Count: {userOrders.Count}");
                            results.AppendLine($"   Customer ID: {customerGuid}");
                            OrderSecurityTest = true;
                        }
                        else
                        {
                            results.AppendLine("❌ Found orders belonging to other users!");
                            results.AppendLine($"   User Orders: {userOrders.Count}");
                            results.AppendLine($"   Total Orders: {ordersResponse.Orders.Count()}");
                            OrderSecurityTest = false;
                        }
                    }
                    else
                    {
                        results.AppendLine("✅ No orders found (expected for new user)");
                        OrderSecurityTest = true;
                    }
                }
                else
                {
                    results.AppendLine("❌ Invalid user identifier format");
                    OrderSecurityTest = false;
                }
            }
            catch (Exception ex)
            {
                results.AppendLine($"❌ Order isolation test failed: {ex.Message}");
                OrderSecurityTest = false;
            }
            results.AppendLine();

            // Overall Results
            results.AppendLine("OVERALL SECURITY STATUS:");
            if (UserIdentityTest && CartSecurityTest && OrderSecurityTest)
            {
                results.AppendLine("🎉 ALL SECURITY TESTS PASSED!");
                results.AppendLine("✅ User isolation is working correctly");
            }
            else
            {
                results.AppendLine("⚠️ SOME SECURITY TESTS FAILED!");
                results.AppendLine("❌ User isolation needs attention");
            }

            TestResults = results.ToString();
            await LoadUserData();
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error running security tests");
            TestResults = $"❌ Security test execution failed: {ex.Message}";
            return Page();
        }
    }

    private async Task LoadUserData()
    {
        try
        {
            CurrentUserId = _userService.GetSecureUserIdentifier();
            CurrentUserEmail = _userService.GetCurrentUserEmail();
            CurrentUserName = _userService.GetCurrentUserName();

            // Load cart items count
            var basket = await _basketService.LoadUserBasket(CurrentUserId);
            CartItemCount = basket.Items.Count;

            // Load orders count
            if (Guid.TryParse(CurrentUserId, out var customerGuid))
            {
                var ordersResponse = await _orderingService.GetOrdersByCustomer(customerGuid);
                OrderCount = ordersResponse?.Orders?.Count() ?? 0;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading user data");
            CartItemCount = 0;
            OrderCount = 0;
        }
    }

    private async Task RunSecurityValidation()
    {
        try
        {
            // Basic validation
            UserIdentityTest = !string.IsNullOrEmpty(CurrentUserId) && !string.IsNullOrEmpty(CurrentUserEmail);

            // Cart validation
            var basket = await _basketService.LoadUserBasket(CurrentUserId);
            CartSecurityTest = string.Equals(basket.UserName, CurrentUserId, StringComparison.OrdinalIgnoreCase);

            // Order validation
            if (Guid.TryParse(CurrentUserId, out var customerGuid))
            {
                var ordersResponse = await _orderingService.GetOrdersByCustomer(customerGuid);
                if (ordersResponse?.Orders != null)
                {
                    OrderSecurityTest = ordersResponse.Orders.All(o => o.CustomerId == customerGuid);
                }
                else
                {
                    OrderSecurityTest = true; // No orders is valid
                }
            }
            else
            {
                OrderSecurityTest = false;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in security validation");
            UserIdentityTest = false;
            CartSecurityTest = false;
            OrderSecurityTest = false;
        }
    }
}
