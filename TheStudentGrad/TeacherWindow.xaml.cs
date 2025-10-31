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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TheStudentGrad.Data;
using TheStudentGrad.Models;

namespace TheStudentGrad
{
    /// <summary>
    /// Interaction logic for TeacherWindow.xaml
    /// </summary>
    public partial class TeacherWindow : Window
    {
        public TeacherWindow()
        {
            InitializeComponent();
            LoadData();
        }
        public void LoadData()
        {
            using (var db = new MyDbcontext())
            {
                cmb_Sub.ItemsSource=db.Course.ToList();
                cmb_Sub.DisplayMemberPath="CourseName";
                cmb_Sub.SelectedValuePath="CourseID";
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_Sub.SelectedItem == null)
            {
                MessageBox.Show("Please Select Subject!");
                return;
            }

            using (var db = new MyDbcontext())
            {
                int selectedcourseID = (int)cmb_Sub.SelectedValue;

                var grades = db.Grade
                    .Include(g => g.Enrollment)
                        .ThenInclude(e => e.User)
                    .Include(g => g.Enrollment)
                        .ThenInclude(e => e.Course)
                    .Where(g => g.Enrollment.CourseId == selectedcourseID)
                    .ToList();

                var Showdata = grades.Select(g => new
                {
                    EnrollmentIDD = g.EnrollmentId,
                    StudentName = g.Enrollment.User.UserName,
                    CourseName = g.Enrollment.Course.CourseName,
                    Assigment = g.AssignmentName,
                    Score = g.Score,
                    MaxScore = g.maxscore,
                    Average = grades
                        .Where(t => t.Enrollment.UserId == g.Enrollment.UserId
                                 && t.Enrollment.CourseId == g.Enrollment.CourseId)
                        .Average(x => x.Score / x.maxscore * 100)
                })
                .ToList();

                data_grid.ItemsSource = Showdata;
            }
        }

        private void data_grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (data_grid.SelectedItem==null)
            {
                return;
            }
            dynamic ff = data_grid.SelectedItem;
            txt_Score.Text=ff.Score.ToString();
            txt_assname.Text=ff.Assigment;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txt_assname==null)
            {
                MessageBox.Show("Please Enter AssigmentName");
                return;
            }
            using (var db = new MyDbcontext())
            {
                var enrolment = db.Enrollment.Where(t => t.CourseId==(int)cmb_Sub.SelectedValue).ToList();
                foreach (var enroll in enrolment)
                {
                    var newGrade = new Grade
                    {
                        EnrollmentId=enroll.EnrollmentID,
                        AssignmentName=txt_assname.Text,
                        Score=10,
                        maxscore=100
                    };
                    db.Grade.Add(newGrade);
                }
                db.SaveChanges();
                MessageBox.Show("Assigment Added Successfull Alhamdollah");
                return;
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (cmb_Sub.SelectedItem==null||string.IsNullOrEmpty(txt_assname.Text)||string.IsNullOrEmpty(txt_Score.Text))
            {
                MessageBox.Show("All Data Must Be Compeleted Please !");
                return;
            }
            if (data_grid.SelectedItem==null)
            {
                MessageBox.Show("Please Select Student ");
                return;
            }
            using (var db=new MyDbcontext())
            {
                dynamic selecteStudent=data_grid.SelectedItem;
                int EnrollmentId = selecteStudent.EnrollmentIDD;
                var UpdateGrade = db.Grade.FirstOrDefault(t => t.EnrollmentId==EnrollmentId&&t.AssignmentName==txt_assname.Text);
                if (UpdateGrade==null)
                {
                    MessageBox.Show("There is No Course To Update ItsGrade!");
                    return;
                }
                UpdateGrade.Score=int.Parse(txt_Score.Text);
                db.SaveChanges();
                MessageBox.Show("Score Updated Successfully");
                LoadData();
                return;


            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if(cmb_Sub.SelectedItem==null||string.IsNullOrEmpty(txt_assname.Text))
            {
                MessageBox.Show("Please All Data Must Be Compeleted!");
                return;
            }
            dynamic SelectedTodelet = data_grid.SelectedItem;
            int EnrollmentIdDelete= SelectedTodelet.EnrollmentIDD;
            using (var db=new MyDbcontext())
            {
                var deleted = db.Grade.FirstOrDefault(t => t.EnrollmentId==EnrollmentIdDelete&&t.AssignmentName==txt_assname.Text);
                if(deleted==null)
                {
                    MessageBox.Show("NO Enrollment Allready Found !");
                    return;
                }
                db.Grade.Remove(deleted);

                db.SaveChanges();
                MessageBox.Show("Enrolment Deleted SuccessFully❤️");
                
                return;
            }
        }
    }
}