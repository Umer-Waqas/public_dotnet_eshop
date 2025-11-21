using eshop.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.application.Interfaces
{
    public interface IInvoiceRepository
    {
        Task AddAsync(Invoice invoice, CancellationToken ct = default);
        Task<Invoice?> GetByIdAsync(Guid id, CancellationToken ct = default);
    }
}
