namespace Karim.ECommerce.Shared.Params
{
    public class CategorySpecParams
    {
        public int? BrandId { get; set; }

		private string? search;
		public string? Search
		{
			get { return search; }
			set { search = value?.ToUpper(); }
		}

		public int PageIndex { get; set; } = 1;
		private int pageSize = 10;
		private const int maxPageSize = 15;
		public int PageSize
		{
			get { return pageSize; }
			set { pageSize = value > maxPageSize ? maxPageSize : value; }
		}


	}
}
