using eshop.application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.infrastructure.EmailSender
{
    public class DummyEmailSender : IEmailSender { public Task SendAsync(Guid customerId, string message, CancellationToken ct = default) { Console.WriteLine($"Email to {customerId}: {message}"); return Task.CompletedTask; } }

}
