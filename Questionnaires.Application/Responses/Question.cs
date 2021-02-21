using Questionnaires.Common;

namespace Questionnaires.Application.Responses
{
    public class Question
    {
        public int QuestionId { get; set; }

        public int SubjectId { get; set; }

        public string Description { get; set; }

        public AnswerCategoryType AnswerCategoryType { get; set; }

        public AnswerOption[] AnswerOptions { get; set; }
    }
}