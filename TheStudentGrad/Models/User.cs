using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStudentGrad.Models
{
    public class User
    {
        [Key]
        public int UserID {  get; set; }
        [Required]
       public string? UserName {  get; set; }
        [Required]
       public string ?Password { get; set; }
        [Required]
       public string? Email {  get; set; }
        [Required]
       public string ?Role { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
        public ICollection<Course>? Courses { get; set; }
    }
}
