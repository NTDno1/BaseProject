using DemoCICD.Contract.Share;
using DemoCICD.Domain.Abstractions;
using DemoCICD.Domain.Abstractions.Repositories;
using DemoCICD.Persistance;
using MediatR;
using static DemoCICD.Contract.Services.Product.Command;

namespace DemoCICD.Application.UserCases.V1.Commands.Product;
public sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepository;

    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(
        IRepositoryBase<Domain.Entities.Product, Guid> productRepository,
        IPublisher publisher,
        IUnitOfWork unitOfWork,
        ApplicationDbContext context)
    {
        _productRepository = productRepository;
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Domain.Entities.Product.CreateProduct(Guid.NewGuid(), request.Name, request.Price, request.Description);
        _productRepository.Add(product);
        //await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success("Tạo thành công Product Id:" + product.Id);
    }
}
