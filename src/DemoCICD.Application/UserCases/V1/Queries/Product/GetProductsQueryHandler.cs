using AutoMapper;
using DemoCICD.Contract.Services.Product;
using DemoCICD.Contract.Share;
using DemoCICD.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoCICD.Application.UserCases.V1.Queries.Product;
public sealed class GetProductsQueryHandler : IQueryHandler<Query.GetProductQuery, List<Response.ProductResponse>>
{
    private readonly IRepositoryBase<Domain.Entities.Identity.Product, Guid> _productRepository;

    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IRepositoryBase<Domain.Entities.Identity.Product, Guid> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<Response.ProductResponse>>> Handle(Query.GetProductQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.FindAll().ToListAsync();

        var result = _mapper.Map<List<Response.ProductResponse>>(products);
        return result;
    }
}
