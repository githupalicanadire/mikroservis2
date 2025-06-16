namespace Shopping.Web.Pages
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class OrderListModel
        (IOrderingService orderingService, IUserService userService, ILogger<OrderListModel> logger)
        : PageModel
    {
        public IEnumerable<OrderModel> Orders { get; set; } = default!;
        public string? CurrentUserName { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var userIdentifier = userService.GetSecureUserIdentifier();
                var userName = userService.GetCurrentUserName() ?? userService.GetCurrentUserEmail();
                CurrentUserName = userName;

                logger.LogInformation("Loading orders for user: {UserId}", userIdentifier);

                // Parse userIdentifier as GUID for the ordering service
                if (Guid.TryParse(userIdentifier, out var customerGuid))
                {
                    var response = await orderingService.GetOrdersByCustomer(customerGuid);

                    if (response?.Orders != null)
                    {
                        // Additional security check: verify all returned orders belong to current user
                        var userOrders = response.Orders.Where(order =>
                            order.CustomerId == customerGuid).ToList();

                        if (userOrders.Count != response.Orders.Count())
                        {
                            logger.LogWarning("Order service returned orders that don't belong to user {UserId}", userIdentifier);
                        }

                        OrderList = userOrders;
                        logger.LogInformation("Successfully loaded {OrderCount} orders for user {UserId}",
                            OrderList.Count(), userIdentifier);
                    }
                    else
                    {
                        logger.LogInformation("No orders found for user {UserId}", userIdentifier);
                        OrderList = new List<OrderModel>();
                    }
                }
                else
                {
                    logger.LogError("Invalid user identifier format: {UserId}", userIdentifier);
                    return RedirectToPage("/Login");
                }

            logger.LogInformation("Loading order list for user: {UserName} (ID: {UserId})", userName, userId);

            try
            {
                var customerId = Guid.Parse(userId);
                var response = await orderingService.GetOrdersByCustomer(customerId);
                Orders = response.Orders ?? new List<OrderModel>();

                logger.LogInformation("Loaded {OrderCount} orders for user: {UserName}", Orders.Count(), userName);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading orders for user: {UserName}", userName);
                Orders = new List<OrderModel>();
            }

            return Page();
        }

            }
            catch (UnauthorizedAccessException)
            {
                logger.LogWarning("Unauthorized access attempt to order list");
                return RedirectToPage("/Login");
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Could not determine user identity for orders");
                return RedirectToPage("/Login");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading orders");
                OrderList = new List<OrderModel>();
            }

            return Page();
        }
}
