using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.application.Interfaces
{
    public interface IEmailSender { Task SendAsync(Guid customerId, string message, CancellationToken ct = default); }

}
