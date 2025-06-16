namespace Catalog.API.Products.GetProducts;

public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // Get all products
        app.MapGet("/products", async (ISender sender) =>
        {
            var query = new GetProductsQuery();

            var result = await sender.Send(query);

            var response = result.Adapt<GetProductsResponse>();

            return Results.Ok(response);
        })
        .WithName("GetAllProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get All Products")
        .WithDescription("Get all products without pagination");

        // Get paginated products
        app.MapGet("/products/paginated", async ([AsParameters] GetProductsPaginatedQuery query, ISender sender) =>
        {
            var result = await sender.Send(query);

            var response = result.Adapt<GetProductsResponse>();

            return Results.Ok(response);
        })
        .WithName("GetPaginatedProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Paginated Products")
        .WithDescription("Get products with pagination");
    }
}
