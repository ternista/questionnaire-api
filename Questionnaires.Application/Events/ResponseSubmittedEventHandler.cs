using System.Threading;
using System.Threading.Tasks;
using Questionnaires.Common;
using Questionnaires.Domain.DomainEvents;
using Questionnaires.Domain.Models;
using Questionnaires.Domain.Models.Query;
using Questionnaires.Domain.Repository;

namespace Questionnaires.Application.Events
{
    public class ResponseSubmittedEventHandler : IHandleEvent<ResponseSubmittedEvent>
    {
        private readonly IQuestionsRepository _questionsRepository;
        private readonly IResponsesRepository _responsesRepository;
        private readonly IResponseSummaryRepository _responseSummaryRepository;

        public ResponseSubmittedEventHandler(IResponsesRepository responsesRepository,
            IResponseSummaryRepository responseSummaryRepository,
            IQuestionsRepository questionsRepository)
        {
            _responsesRepository = responsesRepository;
            _responseSummaryRepository = responseSummaryRepository;
            _questionsRepository = questionsRepository;
        }

        public async Task Handle(ResponseSubmittedEvent @event, CancellationToken cancellationToken)
        {
            var question = await _questionsRepository.GetById(@event.QuestionId, cancellationToken);
            if (question.AnswerCategoryType == AnswerCategoryType.SingleChoiceAnswer)
            {
                var aggregatedResults =
                    await _responsesRepository.GetDepartmentAggregatedResults(@event.QuestionId, @event.DepartmentId,
                        cancellationToken);

                await SaveSummary(aggregatedResults, @event.DepartmentId, @event.QuestionId, cancellationToken);
            }
        }

        private async Task SaveSummary(AggregatedCounts aggregatedResults, int departmentId, int questionId, CancellationToken cancellationToken)
        {
            var responseSummary = new DepartmentQuestionResponseSummary(departmentId, questionId, aggregatedResults);
            
            if (await _responseSummaryRepository.Exists(questionId, departmentId, cancellationToken))
            {
                _responseSummaryRepository.Update(responseSummary);
            }
            else
            {
                _responseSummaryRepository.Add(responseSummary);
            }

            await _responseSummaryRepository.SaveChanges(cancellationToken);
        }
    }
}