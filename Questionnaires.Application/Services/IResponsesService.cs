using System.Threading;
using System.Threading.Tasks;
using Questionnaires.Application.Requests;
using Questionnaires.Application.Responses;

namespace Questionnaires.Application.Services
{
    public interface IResponsesService
    {
        Task SubmitResponses(SubmitUsereResponsesCommand command,
            CancellationToken cancellationToken = default);

        Task<QuestionResponsesSummary[]> GetResponsesSummary(int questionnaireId, CancellationToken cancellationToken);
    }
}