using DemoCICD.Contract.Abstractions.Messages;
using DemoCICD.Contract.Services.V2.Product;

namespace DemoCICD.Application.UserCases.V2.Events;
internal class SendEmailWhenProductChangedEventHandler
    : IDomainEventHandler<DomainEvent.ProductCreated>,
    IDomainEventHandler<DomainEvent.ProductDeleted>
{
    public async Task Handle(DomainEvent.ProductCreated notification, CancellationToken cancellationToken)
    {
        SendEmail();
        await Task.Delay(5000);
    }

    public async Task Handle(DomainEvent.ProductDeleted notification, CancellationToken cancellationToken)
    {
        SendEmail();
        await Task.Delay(5000);
    }

    private void SendEmail()
    {

    }
}
