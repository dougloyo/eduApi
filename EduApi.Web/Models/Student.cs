﻿using System;
using System.ComponentModel.DataAnnotations;
using EduApi.Web.Data.Models;

namespace EduApi.Web.Models
{
    /// <summary>
    /// The Student model containing properties like the StudentId, Name, ...
    /// </summary>
    public class Student : BaseEntity
    {
        /// <summary>
        /// The client's student unique Id. 
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string StudentId { get; set; }

        /// <summary>
        /// The student's first name.
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        /// <summary>
        /// The student's last name.
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        /// <summary>
        /// The student's date of birth.
        /// </summary>
        [Required]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// The student's gender (Use: 1 for Male, 2 for Female).
        /// </summary>
        [Required]
        [Range(1,2)]
        public int Gender { get; set; }

        /// <summary>
        /// The student's email address.
        /// </summary>
        [Required]
        [MaxLength(75)]
        public string Email { get; set; }

        /// <summary>
        /// The student's grade level (Use:  -2 for PK,  -1 for KG,  1 for 1st Grade,  2 for 2nd Grade,  3 for 3rd Grade, 4 for 4th Grade, 5 for 5th Grade, 6 for 6th Grade, 7 for 7th Grade,8 for 8th Grade, 9 for 9th Grade,10 for 10th Grade,11 for 11th Grade12 for 12th Grade)
        /// </summary>
        [Required]
        [Range(-2,12)]
        public int Grade { get; set; }
    }
}