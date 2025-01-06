using AutoMapper;
using DemoCICD.Contract.Services.Product;
using DemoCICD.Contract.Share;
using DemoCICD.Domain.Abstractions.Repositories;
using DemoCICD.Domain.Exceptions;
using DemoCICD.Persistance;

namespace DemoCICD.Application.UserCases.V1.Queries.Product;
public class GetProductByIdQueryHandler : IQueryHandler<Query.GetProductById, Response.ProductResponse>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepository;

    private readonly IMapper _mapper;

    private readonly ApplicationDbContext _context;

    public GetProductByIdQueryHandler(IRepositoryBase<Domain.Entities.Product, Guid> productRepository, IMapper mapper, ApplicationDbContext context)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _context = context;
    }

    public async Task<Result<Response.ProductResponse>> Handle(Query.GetProductById request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(request.Id) ?? throw new ProductException.ProductNotFoundException(request.Id);
        var result = _mapper.Map<Response.ProductResponse>(product);

        return Result.Success(result);
    }
}
