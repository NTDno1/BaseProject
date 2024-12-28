using MediatR;

namespace DemoCICD.Contract.Share;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
