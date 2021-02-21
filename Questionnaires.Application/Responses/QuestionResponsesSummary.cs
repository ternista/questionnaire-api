namespace Questionnaires.Application.Responses
{
    public class QuestionResponsesSummary
    {
        public int QuestionId { get; set; }

        public DepartmentQuestionResponsesSummary[] Deparments { get; set; }
    }
}