using AutoMapper;
using DemoCICD.Contract.Services.V2.Product;
using DemoCICD.Contract.Share;
using DemoCICD.Domain.Abstractions.Dappers;
using DemoCICD.Domain.Exceptions;

namespace DemoCICD.Application.UserCases.V2.Queries;
public sealed class GetProductByIdQueryHandler : IQueryHandler<Query.GetProductById, Response.ProductResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<Response.ProductResponse>> Handle(Query.GetProductById request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(request.Id) ?? throw new ProductException.ProductNotFoundException(request.Id);

        var result = _mapper.Map<Response.ProductResponse>(product);

        return Result.Success(result);
    }
}
