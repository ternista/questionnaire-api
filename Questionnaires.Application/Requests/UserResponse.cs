namespace Questionnaires.Application.Requests
{
    public class UserResponse
    {
        public int QuestionId { get; set; }

        public int? AnswerId { get; set; }
        public string Comment { get; set; }
    }
}