using DemoCICD.Contract.Abstractions.Share;
using DemoCICD.Contract.Enumerations;
using DemoCICD.Contract.Share;
using static DemoCICD.Contract.Services.V2.Product.Response;

namespace DemoCICD.Contract.Services.V2.Product;
public static class Query
{
    public record GetProductsQuery(string? SearchTerm, string? SortColumn, SortOrder? SortOrder, int PageIndex, int PageSize) : IQuery<PagedResult<ProductResponse>>;

    public record GetProductById(Guid Id) : IQuery<ProductResponse>;
}
