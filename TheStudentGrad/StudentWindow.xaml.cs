using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TheStudentGrad.Data;
using TheStudentGrad.Models;

namespace TheStudentGrad
{
    /// <summary>
    /// Interaction logic for StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        private User _user;
        public StudentWindow(User user )
        {
            InitializeComponent();
            _user = user;
            string name = _user.UserName;
            Std_name.Content=name +" Page";
            LoadData();
        }
        public void LoadData()
        {
            using (var db=new MyDbcontext())
            {

                var Insertdata = db.Grade.Include(t => t.Enrollment).ThenInclude(U => U.User).Include(t => t.Enrollment).ThenInclude(C => C.Course).Where(t => t.Enrollment.UserId==_user.UserID).ToList();
                var showData = Insertdata.Select(t => new
                {
                   _CourseName=t.Enrollment.Course.CourseName,
                   CourseAssigment=t.AssignmentName,
                   CourseScore=t.Score,
                   CourseMaxScore=t.maxscore,
                   Average= Insertdata.Where(s=>s.Enrollment.UserId==_user.UserID
                   &&s.Enrollment.Course==t.Enrollment.Course)
                   .Average(c=>(double)c.Score/c.maxscore*100)
                }).ToList();
                data_gridS.ItemsSource = showData;

            }

        }
    }
}
