namespace Questionnaires.Common
{
    public class PagedResult<T>
    {
        public PagedResult(T[] items, int totalCount, int limit, int offset)
        {
            TotalCount = totalCount;
            Limit = limit;
            Offset = offset;
            Items = items;
        }

        public int TotalCount { get; }

        public int Limit { get; }

        public int Offset { get; }

        public T[] Items { get; }
    }
}