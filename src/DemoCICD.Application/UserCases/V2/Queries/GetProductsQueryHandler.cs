using AutoMapper;
using DemoCICD.Contract.Services.V2.Product;
using DemoCICD.Contract.Share;
using DemoCICD.Domain.Abstractions.Dappers;

namespace DemoCICD.Application.UserCases.V2.Queries.Product;

public sealed class GetProductsQueryHandler : IQueryHandler<Query.GetProductsQueryDapper, Result<List<Response.ProductResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Result<List<Response.ProductResponse>>>> Handle(Query.GetProductsQueryDapper request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products.GetAllAsync();

        var results = _mapper.Map<List<Response.ProductResponse>>(products);

        return Result.Success(results);
    }
}
