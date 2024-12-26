using DemoCICD.Contract.Share;
using static DemoCICD.Contract.Services.Product.Response;

namespace DemoCICD.Contract.Services.Product;
public static class Query
{
    public record GetProductQuery() : IQuery<List<ProductResponse>>;

    public record GetProductById() : IQuery<ProductResponse>;
}
