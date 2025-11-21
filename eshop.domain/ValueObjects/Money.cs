using eshop.domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.domain.ValueObjects
{
    public sealed record Money(decimal Amount, string Currency)
    {
        public Money Add(Money other)
        {
            if (Currency != other.Currency) throw new DomainException("Currency mismatch");
            return new Money(Amount + other.Amount, Currency);
        }
    }

}
