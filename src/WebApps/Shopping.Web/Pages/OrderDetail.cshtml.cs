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
    public string CurrentUserName { get; set; } = default!;

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

            // Parse user identifier as customer GUID
            if (!Guid.TryParse(userIdentifier, out var customerGuid))
            {
                _logger.LogError("Invalid user identifier format: {UserId}", userIdentifier);
                return RedirectToPage("/Login");
            }

            // Get user's orders and find the specific order
            var response = await _orderingService.GetOrdersByCustomer(customerGuid);

            if (response == null || response.Orders == null || !response.Orders.Any())
            {
                _logger.LogWarning("No orders found for customer: {UserId}", userIdentifier);
                TempData["ErrorMessage"] = "No orders found.";
                return RedirectToPage("/OrderList");
            }

            var order = response.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                _logger.LogWarning("Order not found with ID: {OrderId} for user: {UserId}", orderId, userIdentifier);
                TempData["ErrorMessage"] = "Order not found or you don't have permission to view this order.";
                return RedirectToPage("/OrderList");
            }

            // Double-check security: ensure order belongs to current user
            if (order.CustomerId != customerGuid)
            {
                _logger.LogWarning("User {UserId} attempted to access order {OrderId} that belongs to {OwnerId}", 
                    userIdentifier, orderId, order.CustomerId);
                TempData["ErrorMessage"] = "You don't have permission to view this order.";
                return RedirectToPage("/OrderList");
            }

            Order = order;
            CurrentUserName = _userService.GetCurrentUserName() ?? _userService.GetCurrentUserEmail() ?? "Unknown";

            _logger.LogInformation("Order detail loaded successfully for user: {UserId}, order: {OrderId}", userIdentifier, orderId);

            return Page();
        }
        catch (UnauthorizedAccessException)
        {
            _logger.LogWarning("Unauthorized access attempt to order detail");
            return RedirectToPage("/Login");
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Could not determine user identity for order detail");
            return RedirectToPage("/Login");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting order details for ID: {OrderId}", orderId);
            TempData["ErrorMessage"] = "An error occurred while retrieving the order details. Please try again later.";
            return RedirectToPage("/OrderList");
        }
    }
}
