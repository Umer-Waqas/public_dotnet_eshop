using eshop.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.infrastructure.Persistence.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(p => p.Sku)
            .IsRequired()
            .HasMaxLength(100);

            entity.OwnsOne(p => p.Price);

            entity.Property(x => x.Stock)
                  .IsRequired();

            entity.Property(x => x.IsActive)
             .IsRequired();
        }
    }
}