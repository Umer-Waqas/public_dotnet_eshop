using eshop.application.EventHandler;
using eshop.application.Interfaces;
using eshop.domain.common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.infrastructure.Events
{
    public class DomainEventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        public DomainEventDispatcher(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken ct = default)
        {
            foreach (var domainEvent in domainEvents)
            {
                var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
                var handlers = _serviceProvider.GetServices(handlerType);
                foreach (var handler in handlers)
                {
                    var method = handler?.GetType().GetMethod("Handle");
                    var task = (Task)method!.Invoke(handler, new object[] { domainEvent, ct })!;
                    await task;
                }
            }
        }
    }
}
