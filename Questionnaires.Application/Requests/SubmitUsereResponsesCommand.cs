namespace Questionnaires.Application.Requests
{
    public class SubmitUsereResponsesCommand
    {
        public int QuestionnaireId { get; set; }

        public int UserId { get; set; }

        public int DepartmentId { get; set; }

        public UserResponse[] Responses { get; set; }
    }
}