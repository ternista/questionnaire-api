using System;
using System.Collections.Generic;
using System.Linq;
using Questionnaires.Application.Requests;
using Questionnaires.Application.Responses;
using Questionnaires.Domain.Models;

namespace Questionnaires.Application.Mapping
{
    public static class ResponseMappingExtensions
    {
        public static Response ToDomain(this UserResponse userResponse, int userId, int departmentId)
        {
            return new(userId, departmentId, userResponse.QuestionId, userResponse.AnswerId, userResponse.Comment);
        }

        public static QuestionResponsesSummary[] ToApplicationResponse(
            this IEnumerable<DepartmentQuestionResponseSummary> summaries)
        {
            return
                summaries
                    .GroupBy(x => x.QuestionId)
                    .Select(grouping => ToQuestionSummary(grouping.Key, grouping))
                    .ToArray();
        }

        private static QuestionResponsesSummary ToQuestionSummary(int questionId,
            IEnumerable<DepartmentQuestionResponseSummary> summaries)
        {
            return new()
            {
                QuestionId = questionId,
                Deparments = summaries.Select(ToDepartmentSummary).ToArray()
            };
        }

        private static DepartmentQuestionResponsesSummary ToDepartmentSummary(DepartmentQuestionResponseSummary d)
        {
            return new()
            {
                DepartmentId = d.DepartmentId,
                Average = Math.Round(d.Average, 2),
                Max = d.Max,
                Min = d.Min,
                TotalResponses = d.TotalResponses
            };
        }
    }
}