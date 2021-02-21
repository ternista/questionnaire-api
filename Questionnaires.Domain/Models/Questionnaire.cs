using System.Collections.Generic;

namespace Questionnaires.Domain.Models
{
    public class Questionnaire
    {
        public int QuestionnaireId { get; internal set; }

        public List<Question> Questions { get; internal set; }

        internal Questionnaire()
        {
        }
    }
}