using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Questionnaires.Application.Requests;
using Questionnaires.Application.Responses;
using Questionnaires.Application.Services;
using Questionnaires.Common;

namespace Questionnaires.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("questionnaire/{questionnaireId}/questions")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionsService _questionsService;

        public QuestionsController(IQuestionsService questionsService)
        {
            _questionsService = questionsService;
        }

        [HttpGet]
        public Task<PagedResult<Question>> GetQuestions(int questionnaireId, [FromQuery] PageOptions pageOptions,
            [FromQuery] string locale = "en-US", CancellationToken cancellationToken = default)
        {
            return _questionsService.GetQuestions(new GetQuestionsRequest
            {
                Locale = locale,
                Offset = pageOptions.Offset ?? 0,
                Limit = pageOptions.Limit ?? 5,
                QuestionnaireId = questionnaireId
            }, cancellationToken);
        }
    }
}