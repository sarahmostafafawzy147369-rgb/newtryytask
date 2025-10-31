using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using TheStudentGrad.Models;

namespace TheStudentGrad.Data
{
    public class MyDbcontext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; }
        public DbSet<Grade> Grade { get; set; }
        protected override void OnConfiguring (DbContextOptionsBuilder OptionBuilder)
        {
            OptionBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=Thelast_taskGrade;Integrated Security=True;Trust Server Certificate=True");
        }
    }
}
