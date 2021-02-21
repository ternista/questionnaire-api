using System.Threading;
using System.Threading.Tasks;
using Questionnaires.Application.Requests;
using Questionnaires.Application.Responses;
using Questionnaires.Application.Mapping;
using Questionnaires.Common;
using Questionnaires.Domain.Models.Query;
using Questionnaires.Domain.Repository;

namespace Questionnaires.Application.Services
{
    public class QuestionsService : IQuestionsService
    {
        private readonly IQuestionsRepository _questionsRepository;

        public QuestionsService(IQuestionsRepository questionsRepository)
        {
            _questionsRepository = questionsRepository;
        }

        public async Task<PagedResult<Question>> GetQuestions(GetQuestionsRequest request,
            CancellationToken cancellationToken = default)
        {
            var questionsQueryResult = await _questionsRepository.GetQuestions(new GetQuestionsQuery
            {
                QuestionnaireId = request.QuestionnaireId,
                Locale = request.Locale,
                Limit = request.Limit,
                Offset = request.Offset
            }, cancellationToken);

            return questionsQueryResult.ToPagedResult();
        }
    }
}