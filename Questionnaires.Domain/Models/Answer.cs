using System.Collections.Generic;
using Questionnaires.Common;

namespace Questionnaires.Domain.Models
{
    public class Answer : IQuestionnaireItem
    {
        internal Answer()
        {
        }
        
        public Answer(int questionId, int orderNumber, AnswerType answerType)
        {
            QuestionId = questionId;
            AnswerType = answerType;
            OrderNumber = orderNumber;
        }

        public int AnswerId { get; internal set; }

        public int OrderNumber { get; internal set; }

        public int QuestionId { get; internal set; }

        public AnswerType AnswerType { get; internal set; }

        public QuestionnaireItemType ItemType => QuestionnaireItemType.Answer;

        public List<Translation> Texts { get; internal set; } = new List<Translation>();
    }
}