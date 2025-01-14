using AutoMapper;
using DemoCICD.Contract.Services.V1.Product;
using DemoCICD.Contract.Share;
using DemoCICD.Domain.Abstractions.Repositories;
using DemoCICD.Domain.Exceptions;

namespace DemoCICD.Application.UserCases.V1.Queries.Product;
public class GetProductByIdQueryHandler : IQueryHandler<Query.GetProductById, Response.ProductResponse>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepository;

    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IRepositoryBase<Domain.Entities.Product, Guid> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result<Response.ProductResponse>> Handle(Query.GetProductById request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(request.Id) ?? throw new ProductException.ProductNotFoundException(request.Id);
        var result = _mapper.Map<Response.ProductResponse>(product);

        return Result.Success(result);
    }
}
