using System.Threading;
using System.Threading.Tasks;

namespace Questionnaires.Domain.DomainEvents
{
    public interface IHandleEvent<in T>
    {
        Task Handle(T @event, CancellationToken cancellationToken);
    }
}