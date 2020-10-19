using Schmoli.Services.Core.Requests;

namespace Schmoli.ServiceTemplate.Requests
{
    public class SecondaryItemPagedRequest : PagedRequest
    {
        public enum SecondaryItemOrderByType
        {
            Id,
            Name,
        }

        public SecondaryItemOrderByType OrderBy { get; set; } = SecondaryItemOrderByType.Id;

        public SortOrder SortOrder { get; set; } = SortOrder.Ascending;

        public string Query { get; set; } = null;
    }
}
