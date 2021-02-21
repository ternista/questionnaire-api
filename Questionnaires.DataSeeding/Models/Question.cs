using System.Collections.Generic;
using Questionnaires.Common;

namespace Questionnaires.DataSeeding.Models
{
    public class Question : QuestionnaireItemBase
    {
        public int QuestionId { get; set; }
        
        public int SubjectId { get; set; }
        
        public AnswerCategoryType AnswerCategoryType { get; set; }

        public List<Answer> QuestionnaireItems { get; set; }
    }
}