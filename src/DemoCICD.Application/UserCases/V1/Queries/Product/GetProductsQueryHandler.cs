using DemoCICD.Contract.Share;
using MediatR;

namespace DemoCICD.Application.UserCases.V1.Queries.Product;
public sealed class GetProductsQueryHandler : IQueryHandler<GetProductQuery, GetProductsResponse>
{
    Task<Result<GetProductsResponse>> IRequestHandler<GetProductQuery, Result<GetProductsResponse>>.Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
