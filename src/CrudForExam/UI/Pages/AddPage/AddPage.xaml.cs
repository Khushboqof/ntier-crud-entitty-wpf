using Exam.Service.DTOs.StudenDto;
using Exam.Service.Services;
using System.Windows;
using System.Windows.Controls;

namespace CrudForExam.UI.Pages.AddPage
{
    /// <summary>
    /// Interaction logic for AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        public AddPage()
        {
            InitializeComponent();
        }

        public async void Add(object sender, RoutedEventArgs e)
        {
            var service = new StudentService();
            StudentForCcreation student = new StudentForCcreation()
            {
                FirstName = Firstname.Text,
                LastName = Lastname.Text,
                Faculty = Faculty.Text
            };
            await service.CreateAsync(student);
        }

    }
}
