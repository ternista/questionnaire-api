namespace Questionnaires.Domain.Models.Query
{
    public class QueryResult<T>
    {
        public QueryResult(int totalResults, int limit, int offset, T[] items)
        {
            TotalResults = totalResults;
            Limit = limit;
            Offset = offset;
            Items = items;
        }

        public int TotalResults { get; }

        public int Limit { get; }

        public int Offset { get; }

        public T[] Items { get; }
    }
}