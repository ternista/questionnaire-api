using Questionnaires.Domain.Models.Query;

namespace Questionnaires.Domain.Models
{
    public class DepartmentQuestionResponseSummary
    {
        private DepartmentQuestionResponseSummary()
        {
        }
        
        public DepartmentQuestionResponseSummary(int departmentId, int questionId,
            AggregatedCounts aggregatedCounts)
        {
            DepartmentId = departmentId;
            QuestionId = questionId;
            Average = aggregatedCounts.Average;
            Max = aggregatedCounts.Max;
            Min = aggregatedCounts.Min;
            TotalResponses = aggregatedCounts.TotalResponses;
        }

        public int DepartmentId { get; private set; }

        public int QuestionId { get; private set; }

        public Question Question { get; private set; }

        public double Average { get; private set; }

        public int Max { get; private set; }

        public int Min { get; private set; }

        public int TotalResponses { get; private set; }
    }
}