using DemoCICD.Contract.Share;
using DemoCICD.Domain.Abstractions;
using DemoCICD.Domain.Abstractions.Dappers.Repositories.Product;
using DemoCICD.Domain.Abstractions.Repositories;
using static DemoCICD.Contract.Services.Product.Command;

namespace DemoCICD.Application.UserCases.V1.Commands.Product;
public sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepository;

    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(
        IRepositoryBase<Domain.Entities.Product, Guid> productRepository,
        IUnitOfWork unitOfWork,
        IProductRepository productRepository2)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
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

        return Result.Success("Tạo thành công Product Id:" + product.Id);
    }
}
