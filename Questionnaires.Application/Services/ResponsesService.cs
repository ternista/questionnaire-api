using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Questionnaires.Application.Requests;
using Questionnaires.Application.Responses;
using Questionnaires.Application.Mapping;
using Questionnaires.Domain.DomainEvents;
using Questionnaires.Domain.Models;
using Questionnaires.Domain.Repository;

namespace Questionnaires.Application.Services
{
    public class ResponsesService : IResponsesService
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IResponsesRepository _responsesRepository;
        private readonly IResponseSummaryRepository _responseSummaryRepository;

        public ResponsesService(IResponsesRepository responsesRepository,
            IEventDispatcher eventDispatcher,
            IResponseSummaryRepository responseSummaryRepository)
        {
            _responsesRepository = responsesRepository;
            _eventDispatcher = eventDispatcher;
            _responseSummaryRepository = responseSummaryRepository;
        }

        public async Task SubmitResponses(SubmitUsereResponsesCommand command,
            CancellationToken cancellationToken = default)
        {
            if (command.Responses == null || !command.Responses.Any())
            {
                throw new InvalidOperationException($"Response should contain at least one answer");
            }
            
            await DeleteOldResponses(command, cancellationToken);
            var newResponses = AddNewResponses(command);
            
            await _responsesRepository.SaveChanges(cancellationToken);

            var events = newResponses.Select(x => new ResponseSubmittedEvent
            {
                QuestionId = x.QuestionId,
                QuestionnaireId = command.QuestionnaireId,
                UserId = x.UserId,
                DepartmentId = x.DepartmentId
            }).ToArray();

            await _eventDispatcher.PublishEvents(events);
        }

        private Response[] AddNewResponses(SubmitUsereResponsesCommand command)
        {
            var newResponses = command
                .Responses
                .Select(x => x.ToDomain(command.UserId, command.DepartmentId))
                .ToArray();

             _responsesRepository.AddResponses(newResponses);
            
            return newResponses;
        }

        private async Task DeleteOldResponses(SubmitUsereResponsesCommand command, CancellationToken cancellationToken)
        {
            var questionIds = command.Responses.Select(x => x.QuestionId);
            var oldResponses = await _responsesRepository.GetUserResponsesForQuestions(command.UserId, questionIds);

            _responsesRepository.DeleteResponses(oldResponses);
        }

        public async Task<QuestionResponsesSummary[]> GetResponsesSummary(int questionnaireId,
            CancellationToken cancellationToken)
        {
            var summaries = await _responseSummaryRepository.GetAllForQuestionnaire(questionnaireId, cancellationToken);

            return summaries.ToApplicationResponse();
        }
    }
}