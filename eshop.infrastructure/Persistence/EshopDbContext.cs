using eshop.domain.Entities;
using eshop.domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace eshop.infrastructure.Persistence
{
    public class EshopDbContext : DbContext
    {
        public EshopDbContext(DbContextOptions<EshopDbContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Invoice> Invoices => Set<Invoice>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(b =>
            {
                b.HasKey(p => p.Id);
                b.OwnsOne(typeof(Money), "Price"); // value object mapping
                b.Property<int>("Stock");
                b.Property<bool>("IsActive");
                b.Property(p => p.Name);
                b.Property(p => p.Sku);
            });

            // Invoice mapping: store lines as owned collection or separate table
            modelBuilder.Entity<Invoice>(b =>
            {
                b.HasKey(i => i.Id);
                b.Property(i => i.CustomerId);
                b.Property(i => i.CreatedAt);

                b.OwnsMany(typeof(InvoiceLine), "_lines", lb =>
                {
                    lb.WithOwner().HasForeignKey("InvoiceId");
                    lb.Property<Guid>("ProductId");
                    lb.Property<int>("Quantity");
                    lb.OwnsOne(typeof(Money), "Price");
                });
            });
        }
    }
}
