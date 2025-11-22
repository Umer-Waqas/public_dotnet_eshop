using eshop.domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.domain.Entities
{
    public class InvoiceLine
    {
        public InvoiceLine()
        {
            ProductId = Guid.Empty;
            Price = new Money(0, "PKR");
        }

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
