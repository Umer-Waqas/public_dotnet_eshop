using eshop.application.Interfaces;
using eshop.domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.infrastructure.EventHandlers
{
    public class InventoryLogHandler : IDomainEventHandler<StockReducedEvent>
    {
        public Task Handle(StockReducedEvent domainEvent, CancellationToken ct)
        {
            // log to monitoring or audit
            Console.WriteLine($"Stock reduced: {domainEvent.ProductId} qty {domainEvent.Quantity}");
            return Task.CompletedTask;
        }
    }
}
