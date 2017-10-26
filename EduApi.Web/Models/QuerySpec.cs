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

        /// <summary>
        /// Indicate the filter to apply to the resource.
        /// </summary>
        ///<example>Filter="FirstName.Contains('jo') and City == 'London' and Orders.Count >= 10"</example>
        public string Filter { get; set; } = "1 = 1";

        /// <summary>
        /// Indicates the projection properties that will be selected in the return resource.
        /// </summary>
        /// <example>Select="Id, FirstName & " " & LastName as Name"</example>
        public string Select { get; set; }

        /// <summary>
        /// Indicates the orderby properties that will be applied to the resulting resource collection.
        /// </summary>
        /// <example>OrderBy="LastName desc, FirstName desc, Id"</example>
        public string OrderBy { get; set; } = "Id";
    }
}