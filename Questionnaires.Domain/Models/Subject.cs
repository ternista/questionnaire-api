using System.Collections.Generic;
using Questionnaires.Common;

namespace Questionnaires.Domain.Models
{
    public class Subject : IQuestionnaireItem
    {
        internal Subject()
        {
        }
        
        public Subject(int orderNumber)
        {
            OrderNumber = orderNumber;
        }

        public int SubjectId { get; internal set; }

        public int OrderNumber { get; internal set; }
        
        public QuestionnaireItemType ItemType => QuestionnaireItemType.Subject;

        public List<Translation> Texts { get; internal set; } = new List<Translation>();
    }
}