using System.ComponentModel.DataAnnotations;

namespace Schmoli.Services.Core.Requests
{
    public class PagedRequest
    {
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        [Range(1, int.MaxValue)]
        public int PageSize { get; set; } = 100;

        public static PagedRequest Default => new();
    }
}
