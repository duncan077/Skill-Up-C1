namespace AlkemyWallet.Core.Services.ResourceParameters
{
    public class PagesParameters
    {
        private int _pageNumber { get; set; } = 1;

        private int _pageSize = 10;
        const int maxPageSize = 20;
        public int PageNumber
        { get=>_pageNumber; set=>_pageNumber=value<1 ? 1:value; }
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > maxPageSize ? maxPageSize : value;
        }

    }
}
