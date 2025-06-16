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
            // Check if user is authenticated
            if (!userService.IsAuthenticated())
            {
                return RedirectToPage("/Login");
            }

            var userId = userService.GetCurrentUserId();
            var userName = userService.GetCurrentUserName() ?? userService.GetCurrentUserEmail();
            CurrentUserName = userName;

            if (string.IsNullOrEmpty(userId))
            {
                logger.LogWarning("Could not determine user ID for orders");
                // Create a deterministic GUID from user name for demo purposes
                if (!string.IsNullOrEmpty(userName))
                {
                    userId = GenerateGuidFromString(userName);
                }
                else
                {
                    return RedirectToPage("/Login");
                }
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

        private string GenerateGuidFromString(string input)
        {
            // Create a deterministic GUID from string for demo purposes
            using var md5 = System.Security.Cryptography.MD5.Create();
            var hash = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
            return new Guid(hash).ToString();
        }
    }
}
