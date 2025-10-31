using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStudentGrad.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int UserId { get; set; }
        [ForeignKey ("UserId")]
        public User? User { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course? Course { get; set; }
        public ICollection<Grade>? Grades { get; set; }
    }
}
