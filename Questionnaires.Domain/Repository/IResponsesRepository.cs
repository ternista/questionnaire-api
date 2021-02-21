using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Questionnaires.Domain.Models;
using Questionnaires.Domain.Models.Query;

namespace Questionnaires.Domain.Repository
{
    public interface IResponsesRepository
    {
        Task<Response[]> GetUserResponsesForQuestions(int userId, IEnumerable<int> questionIds);

        Task<AggregatedCounts> GetDepartmentAggregatedResults(int questionId, int departmentId,
            CancellationToken cancellationToken = default);

        void AddResponses(IEnumerable<Response> responses);

        void DeleteResponses(IEnumerable<Response> responses);

        Task SaveChanges(CancellationToken cancellationToken = default);
    }
}