using DemoCICD.Contract.Share;

namespace DemoCICD.Application.UserCases.V1.Queries.Product;
public sealed class GetProductQuery : IQuery<GetProductsResponse>
{
    public string ProductId { get; set; }
}
