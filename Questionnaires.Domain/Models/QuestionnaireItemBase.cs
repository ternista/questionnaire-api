using System.Collections.Generic;
using Questionnaires.Common;

namespace Questionnaires.Domain.Models
{
    public interface IQuestionnaireItem
    {
        public int OrderNumber { get; }

        public QuestionnaireItemType ItemType { get; }

        public List<Translation> Texts { get; }
    }
}