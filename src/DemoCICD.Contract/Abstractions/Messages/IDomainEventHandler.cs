using MediatR;

namespace DemoCICD.Contract.Abstractions.Messages;
public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
