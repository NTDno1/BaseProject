using MediatR;

namespace DemoCICD.Contract.Share;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
