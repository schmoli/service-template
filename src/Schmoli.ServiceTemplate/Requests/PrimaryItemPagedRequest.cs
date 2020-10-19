using Schmoli.Services.Core.Requests;

namespace Schmoli.ServiceTemplate.Requests
{

    public class PrimaryItemPagedRequest : PagedRequest
    {
        // match name of entity property
        public enum PrimaryItemOrderByType
        {
            Id,
            Name,
        }

        /// <summary>
        /// Order By Field
        /// </summary>
        /// <value></value>
        public PrimaryItemOrderByType OrderBy { get; set; } = PrimaryItemOrderByType.Id;

        /// <summary>
        /// Sort Order
        /// </summary>
        /// <value></value>
        public SortOrder SortOrder { get; set; } = SortOrder.Ascending;

        /// <summary>
        /// Search Name column (case insensitive contains)
        /// </summary>
        /// <value></value>
        public string Query { get; set; } = null;
    }
}
