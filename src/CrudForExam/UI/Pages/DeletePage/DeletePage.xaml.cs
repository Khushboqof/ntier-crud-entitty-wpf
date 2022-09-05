using Exam.Service.Services;
using System.Windows;
using System.Windows.Controls;

namespace CrudForExam.UI.Pages.DeletePage
{
    /// <summary>
    /// Interaction logic for DeletePage.xaml
    /// </summary>
    public partial class DeletePage : Page
    {
        public DeletePage()
        {
            InitializeComponent();
        }

        public async void Delete(object sender, RoutedEventArgs e)
        {
            var service = new StudentService();
            long id = long.Parse(NameTextBox.Text);
            await service.DeleteAsync(id);
            NameTextBox.Text = "";
        }

    }
}
