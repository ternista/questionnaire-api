using System.Collections.Generic;
using Questionnaires.Common;

namespace Questionnaires.Domain.Models
{
    public class Question : IQuestionnaireItem
    {
        internal Question()
        {
        }
        
        public Question(int questionnaireId, int subjectId, int orderNumber, AnswerCategoryType answerCategoryType)
        {
            QuestionnaireId = questionnaireId;
            SubjectId = subjectId;
            OrderNumber = orderNumber;
            AnswerCategoryType = answerCategoryType;
        }

        public int QuestionId { get; internal set; }

        public int QuestionnaireId { get; internal set; }

        public int SubjectId { get; internal set; }
        
        public Subject Subject { get; internal set; }

        public int OrderNumber { get; internal set; }

        public AnswerCategoryType AnswerCategoryType { get; internal set; }

        public QuestionnaireItemType ItemType => QuestionnaireItemType.Question;

        public List<Translation> Texts { get; internal set; } = new List<Translation>();

        public List<Answer> AnswerOptions { get; internal set; }
    }
}