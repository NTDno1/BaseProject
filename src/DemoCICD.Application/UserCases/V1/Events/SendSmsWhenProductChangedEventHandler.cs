using DemoCICD.Contract.Abstractions.Messages;
using DemoCICD.Contract.Services.V1.Product;

namespace DemoCICD.Application.UserCases.V1.Events;
internal class SendSmsWhenProductChangedEventHandler
    : IDomainEventHandler<DomainEvent.ProductCreated>,
    IDomainEventHandler<DomainEvent.ProductDeleted>
{
    public async Task Handle(DomainEvent.ProductCreated notification, CancellationToken cancellationToken)
    {
        SendSms();
        await Task.Delay(5000);
    }

    public async Task Handle(DomainEvent.ProductDeleted notification, CancellationToken cancellationToken)
    {
        SendSms();
        await Task.Delay(5000);
    }

    private void SendSms()
    {

    }
}
