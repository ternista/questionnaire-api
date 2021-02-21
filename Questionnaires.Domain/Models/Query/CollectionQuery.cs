namespace Questionnaires.Domain.Models.Query
{
    public abstract class CollectionQuery
    {
        public int Offset { get; set; }

        public int Limit { get; set; }
    }
}