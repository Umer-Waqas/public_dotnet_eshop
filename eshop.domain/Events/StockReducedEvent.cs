using eshop.domain.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.domain.Events
{
    public sealed class StockReducedEvent : IDomainEvent
    {
        public Guid ProductId { get; }
        public int Quantity { get; }
        public DateTime OccurredOn { get; } = DateTime.UtcNow;

        public StockReducedEvent(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
