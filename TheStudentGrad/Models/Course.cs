using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStudentGrad.Models
{
    public  class Course
    {
        [Key]
        public int CourseID { get; set; }
        public int TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public User? User { get; set; }
        public string ?CourseName { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
