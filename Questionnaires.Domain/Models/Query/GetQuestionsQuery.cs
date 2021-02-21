namespace Questionnaires.Domain.Models.Query
{
    public class GetQuestionsQuery : CollectionQuery
    {
        public int QuestionnaireId { get; set; }

        public string Locale { get; set; }
    }
}