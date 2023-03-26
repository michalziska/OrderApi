namespace SystemOrder.Resources
{
    public class ProductResourceParameters
    {
        const int maxPageSize = 20;

        public string? MainCategory { get; set; }

        public string? SearchQuery { get; set; }

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 2;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value;
        }

        public string? OrderBy { get; set; }

        public string? Fields { get; set; }
    }
}
