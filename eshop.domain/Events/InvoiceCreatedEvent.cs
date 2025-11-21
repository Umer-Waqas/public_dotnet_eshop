using eshop.domain.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.domain.Events
{
    public sealed class InvoiceCreatedEvent : IDomainEvent
    {
        public Guid InvoiceId { get; }
        public Guid CustomerId { get; }
        public IReadOnlyList<(Guid ProductId, int Quantity)> Lines { get; }
        public DateTime OccurredOn { get; } = DateTime.UtcNow;

        public InvoiceCreatedEvent(Guid invoiceId, Guid customerId, List<(Guid, int)> lines)
        {
            InvoiceId = invoiceId;
            CustomerId = customerId;
            Lines = lines;
        }
    }

}
