namespace Karim.ECommerce.Shared.Params
{
    public class BrandSpecParams
    {
        public int? CategoryId { get; set; }

        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value?.ToUpper(); }
        }


        public int PageIndex { get; set; } = 1;

        private const int maxPageSize = 15;
        private int pageSize = 10;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > maxPageSize ? maxPageSize : value;
        }
    }
}
