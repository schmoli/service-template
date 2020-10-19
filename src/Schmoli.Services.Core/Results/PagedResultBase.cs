
namespace Schmoli.Services.Core.Results
{
    public abstract class PagedResultSetBase

    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPageCount { get; set; }
        public int TotalItemCount { get; set; }
    }
}
