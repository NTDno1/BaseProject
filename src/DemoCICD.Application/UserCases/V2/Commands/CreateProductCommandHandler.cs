using DemoCICD.Contract.Services.V2.Product;
using DemoCICD.Contract.Share;
using DemoCICD.Domain.Abstractions.Repositories;
using MediatR;
using static DemoCICD.Contract.Services.V2.Product.Command;

namespace DemoCICD.Application.UserCases.V2.Commands;
public sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepository;

    private readonly IPublisher _publisher;

    public CreateProductCommandHandler(
        IRepositoryBase<Domain.Entities.Product, Guid> productRepository,
        IPublisher publisher)
    {
        _productRepository = productRepository;

        _publisher = publisher;
    }

    public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Domain.Entities.Product.CreateProduct(Guid.NewGuid(), request.Name + "Name ver 2", request.Price, request.Description);
        _productRepository.Add(product);

        await Task.WhenAll(
            _publisher.Publish(new DomainEvent.ProductCreated(product.Id), cancellationToken),
            _publisher.Publish(new DomainEvent.ProductDeleted(product.Id), cancellationToken));

        return Result.Success("Tạo thành công Product Id:" + product.Id);
    }
}
