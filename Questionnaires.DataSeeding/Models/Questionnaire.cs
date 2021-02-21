using System.Collections.Generic;

namespace Questionnaires.DataSeeding.Models
{
    public class Questionnaire
    {
        public int QuestionnaireId { get; set; }
        public List<Subject> QuestionnaireItems { get; set; }
    }
}