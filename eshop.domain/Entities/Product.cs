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
    public class Product : AggregateRoot
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Sku { get; private set; }
        public Money Price { get; private set; }
        public int Stock { get; private set; }
        public bool IsActive { get; private set; }

        private Product()
        {
            Name = "";
            Sku = "";
            Price = new Money(Amount: 0, Currency: "pkr");
        } // for EF / serialization


        public Product(string name, string sku, Money price, int stock)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Name required");
            if (price.Amount <= 0) throw new DomainException("Price must be positive");
            if (stock < 0) throw new DomainException("Stock cannot be negative");

            Id = Guid.NewGuid();
            Name = name;
            Sku = sku;
            Price = price;
            Stock = stock;
            IsActive = true;
        }

        public void ReduceStock(int qty)
        {
            if (qty <= 0) throw new DomainException("Quantity must be positive");
            if (qty > Stock) throw new DomainException("Insufficient stock");
            Stock -= qty;
            AddDomainEvent(new StockReducedEvent(Id, qty));
        }

        public void IncreaseStock(int qty)
        {
            if (qty <= 0) throw new DomainException("Quantity must be positive");
            Stock += qty;
        }

        public void UpdateDetails(string name, Money price, bool isActive)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Name required");
            if (price.Amount <= 0) throw new DomainException("Price must be positive");
            Name = name;
            Price = price;
            IsActive = isActive;
        }

    }
}
