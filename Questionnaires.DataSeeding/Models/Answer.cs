using Questionnaires.Common;

namespace Questionnaires.DataSeeding.Models
{
    public class Answer : QuestionnaireItemBase
    {
        public int? AnswerId { get; set; }
        
        public int QuestionId { get; set; }
        
        public AnswerType AnswerType { get; set; }
    }
}