using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.application.DTOs.ResponseDTOs
{
    public record InvoiceDto(Guid Id, Guid CustomerId, decimal Total, DateTime CreatedAt);
}
