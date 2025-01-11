using DemoCICD.Contract.Share;
using DemoCICD.Domain.Abstractions.Repositories;
using DemoCICD.Domain.Exceptions;
using DemoCICD.Persistance;
using MediatR;
using static DemoCICD.Contract.Services.V1.Product.Command;

namespace DemoCICD.Application.UserCases.V1.Commands.Product;
public sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepository;

    private readonly ApplicationDbContext _context;

    private readonly IPublisher _publisher;

    public UpdateProductCommandHandler(
        IRepositoryBase<Domain.Entities.Product, Guid> productRepository,
        IPublisher publisher,
        ApplicationDbContext context)
    {
        _productRepository = productRepository;
        _context = context;
        _publisher = publisher;
    }

    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(request.Id) ?? throw new ProductException.ProductNotFoundException(request.Id);
        product.Update(request.Name, request.Price, request.Description);
        await _context.SaveChangesAsync();
        return Result.Success("Cập nhật thành công Product Id: " + product.Id);
    }
}

