namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery : IQuery<GetProductsResult>;
public record GetProductsPaginatedQuery(int? PageNumber = 1, int? PageSize = 12) : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .ToListAsync(cancellationToken);

        return new GetProductsResult(products);
    }
}

internal class GetProductsPaginatedQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetProductsPaginatedQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsPaginatedQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 12, cancellationToken);

        return new GetProductsResult(products);
    }
}
