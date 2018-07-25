using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FASTUniversity.Models
{
    public class Student
    {
        public Student()
        {

        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength =1, ErrorMessage = "Firstname cannot be longer than 50 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrolementDate { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}