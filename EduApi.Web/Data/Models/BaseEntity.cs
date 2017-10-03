using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduApi.Web.Data.Models
{
    public abstract class BaseAuditFields
    {
        /// <summary>
        /// The date the record was created.
        /// </summary>
        [Column(TypeName = "datetime2")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedUtcDateTime { get; set; }
        
        /// <summary>
        /// The application id that created the record.
        /// </summary>
        public int CreatedApplicationId { get; set; }

        /// <summary>
        /// The last date the record was updated.
        /// </summary>
        [Column(TypeName = "datetime2")]
        [DataType(DataType.DateTime)]
        public DateTime? LastUpdatedUtcDateTime { get; set; }
        
        /// <summary>
        /// The application that last updated the record.
        /// </summary>
        public int? LastUpdatedApplicationId { get; set; }

        /// <summary>
        /// The logical delete field.
        /// </summary>
        [DefaultValue(false)]
        public bool Deleted { get; set; }

        /// <summary>
        /// The application that deleted the record.
        /// </summary>
        public int? DeletedApplicationId { get; set; }

        /// <summary>
        /// The date when the record was delted.
        /// </summary>
        [Column(TypeName = "datetime2")]
        [DataType(DataType.DateTime)]
        public DateTime? DeletedUtcDate { get; set; }
    }

    public abstract class BaseEntity : BaseAuditFields
    {
        /// <summary>
        /// The surogate internal application key for all entities.
        /// </summary>
        public int Id { get; set; }
    }
}