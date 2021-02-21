namespace Questionnaires.Domain.Models.Query
{
    public class AggregatedCounts
    {
        public int TotalResponses { get; set; }

        public int Max { get; set; }

        public int Min { get; set; }

        public double Average { get; set; }
    }
}