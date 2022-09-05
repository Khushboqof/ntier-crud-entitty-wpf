using CrudForExam.UI.Controls;
using CrudForExam.UI.Pages.AddPage;
using CrudForExam.UI.Pages.DeletePage;
using CrudForExam.UI.Pages.SavePage;
using Exam.Domain.Entities.Students;
using Exam.Service.Interfaces;
using Exam.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CrudForExam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IStudentService service;
        private readonly SavePage _page;
        private readonly AddPage page;
        private readonly DeletePage page1;
        IEnumerable<Student> students;
        private Thread thread;

        public MainWindow()
        {
            service = new StudentService();
            _page = new SavePage();
            page = new AddPage();
            page1 = new DeletePage();
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            students = await service.GetAllAsync();

            Thread thread = new Thread(async () =>
            {
                this.Dispatcher.Invoke(() => { UserList.Items.Clear(); });

                foreach (Student student in students)
                {
                    await this.Dispatcher.InvokeAsync(async () =>
                    {
                        Users model = new Users();
                        model.NameTxt.Text = $"{student.FirstName} {student.LastName}";
                        model.UserImage.ImageSource = student.ImageId is null ? new BitmapImage(new Uri("https://talabamiz.uz/Images//99daf8ac38de4433aa36a61baf4c9c4d.png")) :
                                                                              new BitmapImage(new Uri($"https://talabamiz.uz/{student.Image.Path}"));
                        UserList.Items.Add(model);
                    });
                }
            });
            thread.Start();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserList.Items.Clear();

            string text = Search.Text.ToLower();


            thread = new Thread(async () =>
            {
                students = await service.GetAllAsync();

                students = students.Where(p => p.FirstName.ToLower().Contains(text)
                    || p.LastName.ToLower().Contains(text)
                    || p.Faculty.ToLower().Contains(text));

                await LoadStudents(students);
            });
            thread.Start();
        }

        private async Task LoadStudents(IEnumerable<Student> allStudent)
        {
            foreach (var student in allStudent)
            {
                await this.Dispatcher.InvokeAsync(() =>
                {
                    Users privateChat = new Users();

                    privateChat.NameTxt.Text = student.FirstName + " " + student.LastName;

                    privateChat.UserImage.ImageSource = student.Image is not null
                        ? new BitmapImage(new Uri("https://talabamiz.uz/" + student.Image.Path))
                        : new BitmapImage(new Uri("https://talabamiz.uz/Images//99daf8ac38de4433aa36a61baf4c9c4d.png"));

                    UserList.Items.Add(privateChat);
                });
            }

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameName.Content = _page;
        }

        private void AddPage(object sender, RoutedEventArgs e)
        {
            FrameName.Content = page;
        }

        private void DeletePage(object sender, RoutedEventArgs e)
        {
            FrameName.Content = page1;
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
