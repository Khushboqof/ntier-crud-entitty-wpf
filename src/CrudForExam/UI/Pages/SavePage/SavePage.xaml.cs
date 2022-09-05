using Exam.Domain.Entities.Students;
using Exam.Service.Interfaces;
using Exam.Service.Services;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace CrudForExam.UI.Pages.SavePage
{
    /// <summary>
    /// Interaction logic for SavePage.xaml
    /// </summary>
    public partial class SavePage : Page
    {
        string passportPath;
        string portraitPath;
        IStudentService studentService;

        public SavePage()
        {
            studentService = new StudentService();
            InitializeComponent();
        }

        private void Pasport_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PassportBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.PNG,*.JPG)|*.JPG;*.PNG";

            if (openFileDialog.ShowDialog() == true)
            {
                passportPath = openFileDialog.FileName;
            }
        }

        private void PortraitBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.PNG,*.JPG)|*.JPG;*.PNG";

            if (openFileDialog.ShowDialog() == true)
            {
                portraitPath = openFileDialog.FileName;
            }
        }

        public async void Save(object sender, RoutedEventArgs e)
        {
            Student student = new Student()
            {
                FirstName = Firstname.Text,
                LastName = Lastname.Text,
                Faculty = Faculty.Text
            };


            await studentService.UpdateAsync(long.Parse(StudentId.Text), student, portraitPath, passportPath);
        }
    }
}
