using eshop.application.Interfaces;
using eshop.domain.Entities;
using eshop.infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.infrastructure.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly EshopDbContext _db;
        public InvoiceRepository(EshopDbContext db) => _db = db;

        public async Task AddAsync(Invoice invoice, CancellationToken ct = default)
        {
            await _db.Invoices.AddAsync(invoice, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task<Invoice?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _db.Invoices
                            .Include(i => EF.Property<IEnumerable<InvoiceLine>>(i, "_lines"))
                            .FirstOrDefaultAsync(i => i.Id == id, ct);
        }
    }

}
