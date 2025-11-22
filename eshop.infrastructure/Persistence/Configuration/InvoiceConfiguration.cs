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
    internal class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {

        public void Configure(EntityTypeBuilder<Invoice> entity)
        {
            entity.HasKey(i => i.Id);

            entity.Property(i => i.CustomerId)
                   .IsRequired();

            entity.Property(i => i.CreatedAt)
                   .IsRequired();

            //entity.HasMany(x => x.Lines)
            //   .WithOne()
            //   .HasForeignKey("InvoiceId")
            //   .OnDelete(DeleteBehavior.Cascade);


            entity.Navigation(x => x.Lines)
                  .HasField("_lines")
                  .UsePropertyAccessMode(PropertyAccessMode.Field);

            entity.OwnsMany(x=>x.Lines, line =>
            {
                line.WithOwner().HasForeignKey("InvoiceId");

                line.Property<Guid>("Id").ValueGeneratedOnAdd();
                line.HasKey("Id");

                line.Property(x => x.ProductId)
                     .IsRequired();

                line.OwnsOne(l => l.Price);

                line.Property(l => l.Quantity).IsRequired();
            });
        }
    }
}