namespace Questionnaires.Application.Requests
{
    public class GetQuestionsRequest
    {
        public int QuestionnaireId { get; set; }

        public string Locale { get; set; }

        public int Offset { get; set; } = 0;

        public int Limit { get; set; } = 5;
    }
}