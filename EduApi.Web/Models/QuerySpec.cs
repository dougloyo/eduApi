namespace EduApi.Web.Models
{
    /// <summary>
    /// Query Specifications allowed for the API.
    /// </summary>
    public class QuerySpec
    {
        /// <summary>
        /// Indicates the maximum number of items that should be returned in the results (defaults to 25).
        /// </summary>
        public int PageSize { get; set; } = 25;

        /// <summary>
        /// Indicates how many pages should be skipped before returning results.
        /// </summary>
        public int PageNumber { get; set; } = 0;
    }
}