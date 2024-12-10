namespace Karim.ECommerce.Shared.Dtos.Common
{
    public class Pagination<T>
    {
        public int Count { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PagesNumber { get; set; }
        public IEnumerable<T>? Data { get; set; }

        public Pagination(int pageIndex, int pageSize, IEnumerable<T> Data, int Count)
        {
            this.Count = Count;
            PageIndex = pageIndex;
            PageSize = pageSize;
            PagesNumber = (int) Math.Ceiling( (double)Count / PageSize);
            this.Data = Data;
        }
    }
}
