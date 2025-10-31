using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStudentGrad.Models
{
    public class Grade
    {
        [Key]
        public int GradeID { get; set; }
        public int EnrollmentId { get; set; }
        [ForeignKey("EnrollmentId")]
        public Enrollment? Enrollment { get; set; }
        public decimal Score { get; set; }
        public int maxscore { get; set; }   
        public string? AssignmentName { get; set; }
    }
}
