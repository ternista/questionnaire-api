namespace Questionnaires.Domain.Models
{
    public class Response
    {
        internal Response()
        {
        }
        
        public Response(int userId, int departmentId, int questionId, int? answerId, string comment)
        {
            UserId = userId;
            DepartmentId = departmentId;
            QuestionId = questionId;
            AnswerId = answerId;
            Comment = comment;
        }

        public int UserId { get; internal set; }

        public int DepartmentId { get; internal set; }

        public int QuestionId { get; internal set; }

        public int? AnswerId { get; internal set; }

        public Answer Answer { get; internal set; }

        public string Comment { get; internal set; }
    }
}