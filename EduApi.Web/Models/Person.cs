using System;
using System.ComponentModel.DataAnnotations;
using EduApi.Web.Data.Models;

namespace EduApi.Web.Models
{
    /// <summary>
    /// The Person model containing properties like the PersonId, Name, ...
    /// </summary>
    public class Person : BaseEntity
    {
        /// <summary>
        /// The client's unique Id for person. 
        /// </summary>
        [MaxLength(50)]
        public string PersonKey { get; set; }

        /// <summary>
        /// The person's first name.
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        /// <summary>
        /// The person's last name.
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        /// <summary>
        /// The person's date of birth.
        /// </summary>
        [Required]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// The person's gender at birth (Use: 1 for Male, 2 for Female).
        /// </summary>
        [Required]
        [Range(1,2)]
        public int GenderAtBirth { get; set; }

        /// <summary>
        /// The person's email address.
        /// </summary>
        [Required]
        [MaxLength(75)]
        public string Email { get; set; }
    }
}