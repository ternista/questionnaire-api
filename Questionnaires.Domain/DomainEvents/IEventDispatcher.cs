using System.Collections.Generic;
using System.Threading.Tasks;

namespace Questionnaires.Domain.DomainEvents
{
    public interface IEventDispatcher
    {
        Task PublishEvents<T>(IEnumerable<T> events) where T : IEvent;
    }
}