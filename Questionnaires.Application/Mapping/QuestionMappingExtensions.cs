using System.Linq;
using Questionnaires.Application.Responses;
using Questionnaires.Common;
using Questionnaires.Domain.Models;
using Questionnaires.Domain.Models.Query;
using Question = Questionnaires.Application.Responses.Question;

namespace Questionnaires.Application.Mapping
{
    public static class QuestionMappingExtensions
    {
        public static PagedResult<Question> ToPagedResult(this QueryResult<Domain.Models.Question> queryResult)
        {
            var mappedResults = queryResult.Items
                .Select(x => x.ToApplicationResponse())
                .ToArray();

            return new PagedResult<Question>(mappedResults, queryResult.TotalResults, queryResult.Limit,
                queryResult.Offset);
        }

        private static Question ToApplicationResponse(this Domain.Models.Question question)
        {
            return new()
            {
                QuestionId = question.QuestionId,
                Description = question.Texts.SingleOrDefault()?.Text,
                SubjectId = question.SubjectId,
                AnswerCategoryType = question.AnswerCategoryType,
                AnswerOptions = question.AnswerOptions
                    .Select(ToAnswerOption)
                    .ToArray()
            };
        }

        private static AnswerOption ToAnswerOption(Answer answer)
        {
            return new()
            {
                AnswerId = answer.AnswerId,
                Description = answer.Texts.SingleOrDefault()?.Text
            };
        }
    }
}