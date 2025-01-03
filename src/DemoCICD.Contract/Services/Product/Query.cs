using DemoCICD.Contract.Abstractions.Share;
using DemoCICD.Contract.Enumerations;
using DemoCICD.Contract.Share;
using static DemoCICD.Contract.Services.Product.Response;

namespace DemoCICD.Contract.Services.Product;
public static class Query
{
    public record GetProductsQuery(string? SearchTerm, string? SortColumn, SortOrder? SortOrder, IDictionary<string, SortOrder>? SortColumnAndOrder, int PageIndex, int PageSize) : IQuery<PagedResult<ProductResponse>>;

    public record GetProductById(Guid Id) : IQuery<ProductResponse>;
}
