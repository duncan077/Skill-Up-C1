namespace AlkemyWallet.Core.Services.ResourceParameters
{
    public class PagesParameters
    {
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        const int maxPageSize = 20;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > maxPageSize ? maxPageSize : value;
        }

    }
}
