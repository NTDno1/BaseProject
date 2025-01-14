using DemoCICD.Contract.Services.V2.Product;
using DemoCICD.Contract.Share;
using DemoCICD.Domain.Abstractions.Dappers;
using DemoCICD.Domain.Exceptions;

namespace DemoCICD.Application.UserCases.V2.Commands;
public sealed class UpdateProductCommandHandler : ICommandHandler<Contract.Services.V2.Product.Command.UpdateProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(Command.UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var getProduct = await _unitOfWork.Products.GetByIdAsync(request.Id) ?? throw new ProductException.ProductNotFoundException(request.Id);
        getProduct.Update(request.Name, request.Price, request.Description);
        await _unitOfWork.Products.UpdateAsync(getProduct);
        return Result.Success("Update thành công Product Id:" + getProduct.Id);
    }
}
