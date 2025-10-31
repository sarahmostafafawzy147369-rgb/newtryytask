using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheStudentGrad.Data;

namespace TheStudentGrad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_name.Text)||string.IsNullOrEmpty(txt_Pass.Text))
            {
                MessageBox.Show("Please All Data Must Be Compeleted");
                return;
            }
            using (var db =new MyDbcontext())
            {
             var check=   db.User.FirstOrDefault(t => t.UserName==txt_name.Text &&t.Password==txt_Pass.Text);
               if(check==null)
                {
                    MessageBox.Show("User Not Found");
                    return;
                }
               if(check.Role=="Teacher")
                {
                    TeacherWindow teacherWindow=new TeacherWindow();
                    teacherWindow.Show();
                    this.Close();
                }
               if(check.Role=="Student")
                {
                    StudentWindow studentWindow=new StudentWindow(check);
                    studentWindow.Show();
                    this.Close();   
                }
            }
        }
    }
}