using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.application.DTOs.RequestDTOs
{
    public record CreateInvoiceItem(Guid ProductId, int Quantity);
}