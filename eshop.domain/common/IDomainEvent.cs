using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.domain.common
{
    public interface IDomainEvent { DateTime OccurredOn { get; } }
}
