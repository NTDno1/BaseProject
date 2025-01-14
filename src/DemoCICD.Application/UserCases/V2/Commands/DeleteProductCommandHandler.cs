using DemoCICD.Contract.Services.V2.Product;
using DemoCICD.Contract.Share;
using DemoCICD.Domain.Abstractions.Dappers;

namespace DemoCICD.Application.UserCases.V2.Commands;
public sealed class DeleteProductCommandHandler : ICommandHandler<Command.DeleteProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(Command.DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Products.DeleteAsync(request.Id);
        return Result.Success("Xóa thành công Product Id: " + request.Id);
    }
}
