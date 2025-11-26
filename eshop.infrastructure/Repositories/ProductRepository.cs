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
    public class ProductRepository : IProductRepository
    {
        private readonly EshopDbContext _db;
        public ProductRepository(EshopDbContext db) => _db = db;

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _db.Products.FindAsync(new object[] { id }, ct);
        }

        public async Task<List<Product>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct = default)
        {
            return await _db.Products
                .Where(p => ids.Contains(p.Id))
                //.Select(p => new Product(
                //    p.Id,
                //    p.Stock,
                //    p.Price
                //))
                .ToListAsync(ct);
        }

        public async Task UpdateAsync(Product product, CancellationToken ct = default)
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync(ct);
        }

        public async Task UpdateRangeAsync(IEnumerable<Product> products, CancellationToken ct = default)
        {
            foreach (var product in products)
            {
                var tracked = _db.Products.Local.FirstOrDefault(p => p.Id == product.Id);
                if (tracked == null)
                {
                    _db.Products.Attach(product);
                }

                // Mark only modified properties as updated (optional, for minimal updates)
                _db.Entry(product).Property(p => p.Stock).IsModified = true;

                // If you have other fields that may change, mark them too
                // _db.Entry(product).Property(p => p.Price).IsModified = true;
            }

            // Save changes will persist all updates in a single batch
            await _db.SaveChangesAsync(ct);
        }
    }
}
