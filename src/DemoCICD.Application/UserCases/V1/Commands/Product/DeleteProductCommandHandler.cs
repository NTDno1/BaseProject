using DemoCICD.Contract.Services.Product;
using DemoCICD.Contract.Share;
using DemoCICD.Domain.Abstractions;
using DemoCICD.Domain.Abstractions.Repositories;
using DemoCICD.Domain.Exceptions;
using DemoCICD.Persistance;

namespace DemoCICD.Application.UserCases.V1.Commands.Product;
public class DeleteProductCommandHandler : ICommandHandler<Command.DeleteProductCommand>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepository;
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(
        IRepositoryBase<Domain.Entities.Product, Guid> productRepository,
        IUnitOfWork unitOfWork,
        ApplicationDbContext context)
    {
        _productRepository = productRepository;
        _context = context;
    }

    public async Task<Result> Handle(Command.DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(request.Id) ?? throw new ProductException.ProductNotFoundException(request.Id);
        _productRepository.Remove(product);
        await _context.SaveChangesAsync();
        return Result.Success("Xóa thành công Product Id: " + product.Id);
    }
}
