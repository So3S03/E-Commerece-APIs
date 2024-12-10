namespace Karim.ECommerce.Shared.Params
{
    public class ProductSpecParams
    {

        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }

        private string? search;
        public string? Search
        {
            get { return search; }
            set { search = value?.ToUpper(); }
        }

        public int PageIndex { get; set; } = 1;

        private const int maxPageSize = 10;
        private int pageSize = 5; //setting Default Number For The Returning Products

        public int PageSize
        {
            get => pageSize; 
            set => pageSize = value > maxPageSize ? maxPageSize : value; //To Force PageSize To Have Max Value (10 Products)
        }
    }
}
