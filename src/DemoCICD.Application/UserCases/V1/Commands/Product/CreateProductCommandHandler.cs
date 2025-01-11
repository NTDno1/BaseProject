using DemoCICD.Contract.Services.V1.Product;
using DemoCICD.Contract.Share;
using DemoCICD.Domain.Abstractions;
using DemoCICD.Domain.Abstractions.Repositories;
using MediatR;
using static DemoCICD.Contract.Services.V1.Product.Command;

namespace DemoCICD.Application.UserCases.V1.Commands.Product;
public sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepository;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IPublisher _publisher;

    public CreateProductCommandHandler(
        IRepositoryBase<Domain.Entities.Product, Guid> productRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Domain.Entities.Product.CreateProduct(Guid.NewGuid(), request.Name, request.Price, request.Description);
        _productRepository.Add(product);

        //await _unitOfWork.SaveChangesAsync(cancellationToken);

        var productSecond = Domain.Entities.Product.CreateProduct(
            Guid.NewGuid(),
            product.Name + " Second",
            product.Price,
            product.Id.ToString());
        _productRepository.Add(productSecond);

        // => Send Email
        //await _publisher.Publish(new DomainEvent.ProductCreated(productSecond.Id), cancellationToken);
        //await _publisher.Publish(new DomainEvent.ProductDeleted(product.Id), cancellationToken);

        await Task.WhenAll(
            _publisher.Publish(new DomainEvent.ProductCreated(productSecond.Id), cancellationToken),
            _publisher.Publish(new DomainEvent.ProductDeleted(product.Id), cancellationToken));

        return Result.Success("Tạo thành công Product Id:" + product.Id);
    }
}
