using System.Threading;
using System.Threading.Tasks;
using Questionnaires.Domain.Models;
using Questionnaires.Domain.Models.Query;

namespace Questionnaires.Domain.Repository
{
    public interface IQuestionsRepository
    {
        Task<QueryResult<Question>> GetQuestions(GetQuestionsQuery query, CancellationToken cancellationToken);
        Task<Question> GetById(int questionId, CancellationToken cancellationToken);
    }
}