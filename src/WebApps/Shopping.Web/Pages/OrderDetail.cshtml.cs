using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Web.Models.Ordering;
using Shopping.Web.Services;

namespace Shopping.Web.Pages;

[Microsoft.AspNetCore.Authorization.Authorize]
public class OrderDetailModel : PageModel
{
    private readonly IOrderingService _orderingService;
    private readonly IUserService _userService;
    private readonly ILogger<OrderDetailModel> _logger;

    public OrderDetailModel(IOrderingService orderingService, IUserService userService, ILogger<OrderDetailModel> logger)
    {
        _orderingService = orderingService;
        _userService = userService;
        _logger = logger;
    }

    public OrderModel Order { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid orderId)
    {
        try
        {
            var userIdentifier = _userService.GetSecureUserIdentifier();

            if (orderId == Guid.Empty)
            {
                _logger.LogWarning("Invalid order ID: {OrderId}", orderId);
                return RedirectToPage("/OrderList");
            }

            _logger.LogInformation("Loading order detail for user: {UserId}, order: {OrderId}", userIdentifier, orderId);

            var response = await _orderingService.GetOrdersByName(orderId.ToString());

            if (response?.Orders == null || !response.Orders.Any())
            {
                _logger.LogWarning("Order {OrderId} not found", orderId);
                return RedirectToPage("/OrderList");
            }

            var order = response.Orders.FirstOrDefault();
            if (order == null)
            {
                _logger.LogWarning("Order {OrderId} not found in response", orderId);
                return RedirectToPage("/OrderList");
            }

            // Critical security check: ensure order belongs to current user
            if (!Guid.TryParse(userIdentifier, out var currentUserGuid) ||
                order.CustomerId != currentUserGuid)
            {
                _logger.LogWarning("User {UserId} attempted to access order {OrderId} that doesn't belong to them. Order belongs to {OwnerId}",
                    userIdentifier, orderId, order.CustomerId);
                return RedirectToPage("/OrderList");
            }

            Order = order;
            var userName = _userService.GetCurrentUserName() ?? _userService.GetCurrentUserEmail();

            // Get user's orders and find the specific order
            var customerId = Guid.Parse(userId);
            var response = await _orderingService.GetOrdersByCustomer(customerId);

            if (response == null || response.Orders == null)
            {
                _logger.LogWarning("No orders found for customer: {UserName}", userName);
                return NotFound();
            }

            var order = response.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                _logger.LogWarning("Order not found with ID: {OrderId} for user: {UserName}", orderId, userName);
                TempData["ErrorMessage"] = "Order not found or you don't have permission to view this order.";
                return RedirectToPage("/OrderList");
            }

            Order = order;
            _logger.LogInformation("Order detail loaded for user: {UserName}, order: {OrderId}", userName, orderId);

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting order details for ID: {OrderId}", orderId);
            TempData["ErrorMessage"] = "An error occurred while retrieving the order details. Please try again later.";
            return RedirectToPage("/OrderList");
        }
    }
}
