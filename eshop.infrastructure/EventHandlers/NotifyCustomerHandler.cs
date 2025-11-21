using eshop.application.EventHandler;
using eshop.application.Interfaces;
using eshop.domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.infrastructure.EventHandlers
{
    public class NotifyCustomerHandler : IDomainEventHandler<InvoiceCreatedEvent>
    {
        private readonly IEmailSender _email;
        public NotifyCustomerHandler(IEmailSender email) => _email = email;

        public async Task Handle(InvoiceCreatedEvent domainEvent, CancellationToken ct)
        {
            // compose email
            await _email.SendAsync(domainEvent.CustomerId, $"Your order {domainEvent.InvoiceId} created", ct);
        }
    }
}
