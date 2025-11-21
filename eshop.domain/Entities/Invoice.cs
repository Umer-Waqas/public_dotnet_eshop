using eshop.domain.common;
using eshop.domain.Events;
using eshop.domain.Exceptions;
using eshop.domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.domain.Entities
{
    // Domain/Entities/Invoice.cs
    public class Invoice : AggregateRoot
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public IReadOnlyList<InvoiceLine> Lines => _lines.AsReadOnly();
        private readonly List<InvoiceLine> _lines = new();

        private Invoice() { }

        public Invoice(Guid customerId)
        {
            if (customerId == Guid.Empty) throw new DomainException("Customer required");
            Id = Guid.NewGuid();
            CustomerId = customerId;
            CreatedAt = DateTime.UtcNow;
        }

        public void AddLine(Guid productId, Money price, int qty)
        {
            if (productId == Guid.Empty) throw new DomainException("Product required");
            if (qty <= 0) throw new DomainException("Quantity must be positive");
            if (price.Amount <= 0) throw new DomainException("Price must be positive");

            var line = new InvoiceLine(productId, price, qty);
            _lines.Add(line);
        }

        public Money Total()
        {
            var amount = _lines.Select(l => l.Price.Amount * l.Quantity).Sum();
            return new Money(amount, _lines.FirstOrDefault()?.Price.Currency ?? "USD");
        }

        public void Complete()
        {
            AddDomainEvent(new InvoiceCreatedEvent(Id, CustomerId, Lines.Select(l => (l.ProductId, l.Quantity)).ToList()));
        }
    }

    public class InvoiceLine
    {
        public Guid ProductId { get; private set; }
        public Money Price { get; private set; }
        public int Quantity { get; private set; }

        public InvoiceLine(Guid productId, Money price, int quantity)
        {
            ProductId = productId;
            Price = price;
            Quantity = quantity;
        }
    }

}
