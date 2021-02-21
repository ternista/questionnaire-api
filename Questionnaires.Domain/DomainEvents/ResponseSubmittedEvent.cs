namespace Questionnaires.Domain.DomainEvents
{
    public class ResponseSubmittedEvent : IEvent
    {
        public int QuestionnaireId { get; set; }

        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public int DepartmentId { get; set; }
    }
}