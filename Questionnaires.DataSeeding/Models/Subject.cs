using System.Collections.Generic;

namespace Questionnaires.DataSeeding.Models
{
    public class Subject : QuestionnaireItemBase
    {
        public int SubjectId { get; set; }
        
        public List<Question> QuestionnaireItems { get; set; }
    }
}