using System.Linq.Expressions;
using AutoMapper;
using DemoCICD.Contract.Abstractions.Share;
using DemoCICD.Contract.Enumerations;
using DemoCICD.Contract.Services.V2.Product;
using DemoCICD.Contract.Share;
using DemoCICD.Domain.Abstractions.Repositories;
using MediatR;

namespace DemoCICD.Application.UserCases.V2.Queries.Product;
public sealed class GetProductsQueryHandler : IRequestHandler<Query.GetProductsQuery, Result<PagedResult<Response.ProductResponse>>>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepository;

    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IRepositoryBase<Domain.Entities.Product, Guid> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<Response.ProductResponse>>> Handle(Query.GetProductsQuery request, CancellationToken cancellationToken)
    {
        var productsQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
        ? _productRepository.FindAll()
        : _productRepository.FindAll(x => x.Name.Contains(request.SearchTerm) || x.Description.Contains(request.SearchTerm));

        productsQuery = request.SortOrder == SortOrder.Descending
        ? productsQuery.OrderByDescending(GetSortProperty(request))
        : productsQuery.OrderBy(GetSortProperty(request));

        var products = await PagedResult<Domain.Entities.Product>.CreateAsync(productsQuery,
            request.PageIndex,
            request.PageSize);

        var result = _mapper.Map<PagedResult<Response.ProductResponse>>(products);
        return Result.Success(result);
    }

    private static Expression<Func<Domain.Entities.Product, object>> GetSortProperty(Query.GetProductsQuery request)
    {
        return request.SortColumn?.ToLower() switch
        {
            "name" => product => product.Name,
            "price" => product => product.Price,
            "description" => product => product.Description,
            _ => product => product.Id
        };
    }
}
