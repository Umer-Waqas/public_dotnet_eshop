using eshop.application.DTOs.RequestDTOs;
using eshop.application.DTOs.ResponseDTOs;
using eshop.application.Interfaces;
using eshop.domain.Entities;
using eshop.domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.application.Services
{
    public class InvoiceService
    {
        private readonly IProductRepository _productRepo;
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IEventDispatcher _dispatcher;

        public InvoiceService(IProductRepository productRepo,
                              IInvoiceRepository invoiceRepo,
                              IEventDispatcher dispatcher)
        {
            _productRepo = productRepo;
            _invoiceRepo = invoiceRepo;
            _dispatcher = dispatcher;
        }

        public async Task<InvoiceDto> CreateInvoiceAsync(CreateInvoiceRequest request, CancellationToken ct = default)
        {
            // validate is typically done before calling service (e.g. pipeline)
            var invoice = new Invoice(request.CustomerId);

            // load products and apply domain behaviour (reduce stock via Domain entity)
            foreach (var item in request.Items)
            {
                var product = await _productRepo.GetByIdAsync(item.ProductId, ct)
                              ?? throw new DomainException($"Product {item.ProductId} not found");

                // business rule in domain
                product.ReduceStock(item.Quantity);

                invoice.AddLine(product.Id, product.Price, item.Quantity);

                // persist changed product state via repo (but we will save after all ops)
                await _productRepo.UpdateAsync(product, ct);
            }

            invoice.Complete();

            await _invoiceRepo.AddAsync(invoice, ct);

            // Dispatch domain events (InvoiceCreated, and StockReduced created when ReduceStock called)
            var events = invoice.DomainEvents;
            await _dispatcher.DispatchAsync(events, ct);

            // clear events after dispatch if desired (in repo/unitofwork)
            invoice.ClearDomainEvents();

            return new InvoiceDto(invoice.Id, invoice.CustomerId, invoice.Total().Amount, invoice.CreatedAt);
        }
    }

}
