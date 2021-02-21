using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Questionnaires.Api.AuthenticationMock;
using Questionnaires.Application.Requests;
using Questionnaires.Application.Responses;
using Questionnaires.Application.Services;

namespace Questionnaires.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("questionnaire/{questionnaireId}/responses")]
    public class ResponsesController : ControllerBase
    {
        private readonly IResponsesService _responsesService;

        public ResponsesController(IResponsesService responsesService)
        {
            _responsesService = responsesService;
        }

        [HttpPost]
        public async Task SubmitResponses(int questionnaireId, [FromBody] UserResponse[] response)
        {
            var userId = User.GetUserId();
            var departmentId = User.GetUserDepartmentId();

            await _responsesService.SubmitResponses(new SubmitUsereResponsesCommand
            {
                QuestionnaireId = questionnaireId,
                DepartmentId = departmentId,
                UserId = userId,
                Responses = response
            });
        }

        [HttpGet]
        [Route("summary")]
        public async Task<QuestionResponsesSummary[]> GetSummary(int questionnaireId,
            CancellationToken cancellationToken = default)
        {
            return await _responsesService.GetResponsesSummary(questionnaireId, cancellationToken);
        }
    }
}