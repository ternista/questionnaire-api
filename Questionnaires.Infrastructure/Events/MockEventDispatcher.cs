using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Questionnaires.Domain.DomainEvents;

namespace Questionnaires.Infrastructure.Events
{
    public class MockEventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MockEventDispatcher> _logger;

        public MockEventDispatcher(IServiceProvider serviceProvider, ILogger<MockEventDispatcher> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        
        public async Task PublishEvents<T>(IEnumerable<T> events) where T : IEvent
        {
            // mocking asynchronous event processing
            var handlers = _serviceProvider.GetServices<IHandleEvent<T>>();
            foreach (var @event in events)
            foreach (var handler in handlers)
            {
                try
                {
                    await handler.Handle(@event, CancellationToken.None);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Failed to process event {@event.GetType()} with handler {handler.GetType()}");
                }
            }
        }
    }
}