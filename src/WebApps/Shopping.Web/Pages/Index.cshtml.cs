using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Web.Models.Catalog;
using Shopping.Web.Services;

namespace Shopping.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ICatalogService catalogService;
    private readonly ILogger<IndexModel> logger;

    public IndexModel(ICatalogService catalogService, ILogger<IndexModel> logger)
    {
        this.catalogService = catalogService;
        this.logger = logger;
    }

    public IEnumerable<ProductModel> ProductList { get; set; } = new List<ProductModel>();
    public IEnumerable<string> Categories { get; set; } = new List<string>();

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            logger.LogInformation("Loading products for home page");

            var response = await catalogService.GetProducts();
            if (response?.Products == null)
            {
                logger.LogWarning("No products returned from catalog service");
                return Page();
            }

            ProductList = response.Products;
            Categories = ProductList.SelectMany(p => p.Category).Distinct();

            return Page();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while loading products");
            return Page();
        }
    }
}
