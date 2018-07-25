using System;
using System.ComponentModel.DataAnnotations;

namespace FASTUniversity.Domain.ViewModels
{
    public class EnrollmentDataGroup
    {     
        [Display(Name = "Enrollment Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EnrollmentData { get; set; }

        [Display(Name = "Student Count")]
        public int StudentCount { get; set; }
    }
}