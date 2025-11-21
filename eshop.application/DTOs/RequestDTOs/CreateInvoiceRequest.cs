using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.application.DTOs.RequestDTOs
{
    public record CreateInvoiceRequest(Guid CustomerId, List<CreateInvoiceItem> Items);
    
}