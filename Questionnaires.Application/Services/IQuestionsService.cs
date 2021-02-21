using System.Threading;
using System.Threading.Tasks;
using Questionnaires.Application.Requests;
using Questionnaires.Application.Responses;
using Questionnaires.Common;

namespace Questionnaires.Application.Services
{
    public interface IQuestionsService
    {
        public Task<PagedResult<Question>> GetQuestions(GetQuestionsRequest request,
            CancellationToken cancellationToken = default);
    }
}