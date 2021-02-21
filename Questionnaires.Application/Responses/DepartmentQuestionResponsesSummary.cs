namespace Questionnaires.Application.Responses
{
    public class DepartmentQuestionResponsesSummary
    {
        public int DepartmentId { get; set; }

        public int Min { get; set; }

        public int Max { get; set; }

        public double Average { get; set; }

        public int TotalResponses { get; set; }
    }
}