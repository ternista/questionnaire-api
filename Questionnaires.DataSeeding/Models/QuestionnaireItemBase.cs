using System.Collections.Generic;
using Questionnaires.Common;

namespace Questionnaires.DataSeeding.Models
{
    public class QuestionnaireItemBase
    {
        public Dictionary<string, string> Texts { get; set; }
        public QuestionnaireItemType ItemType { get; set; }
        public int OrderNumber { get; set; }
    }
}