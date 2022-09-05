using Exam.Data.IRepositories;
using Exam.Data.Repositories;
using Exam.Domain.Entities.Students;
using Exam.Service.Constants;
using Exam.Service.DTOs.StudenDto;
using Exam.Service.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Exam.Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository studentRepo;
        private readonly string url = ApiConst.Base_Url;
        private readonly HttpClient httpClient;

        public StudentService()
        {
            studentRepo = new StudentRepository();
            httpClient = new HttpClient();
        }

        public async Task<Student> CreateAsync(StudentForCcreation student)
        {
            var Student = JsonConvert.SerializeObject(student);

            var requestContent = new StringContent
                (Student, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, requestContent);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var createdStudent = JsonConvert.DeserializeObject<Student>(content);

                await studentRepo.CreateAsync(createdStudent);
                await studentRepo.SaveAsyn();

                return createdStudent;
            }

            return null;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var response = await httpClient.DeleteAsync(url + id);

            if (response.IsSuccessStatusCode)
            {
                await studentRepo.DeleteAsync(s => s.Id == id);
                await studentRepo.SaveAsyn();
                HttpClient httpClient = new HttpClient();
                await httpClient.DeleteAsync(url + id);
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
                ("Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes("admin:12345")));

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var resultContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<Student>>(resultContent);
            }

            return null;
        }

        public async Task<Student> GetAsync(long id)
        {
            var response = await httpClient.GetAsync(url + id);

            if (response.IsSuccessStatusCode)
            {
                var resultContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Student>(resultContent);
            }

            return null;
        }

        public async Task<Student> UpdateAsync(long id, Student student, string portraitPath, string passportPath)
        {

            HttpResponseMessage response = new HttpResponseMessage();
            MultipartFormDataContent formData = new MultipartFormDataContent();
            if (portraitPath != null && passportPath != null)
            {
                formData.Add(new StreamContent(File.OpenRead(portraitPath)), "image", "image.png");
                formData.Add(new StreamContent(File.OpenRead(passportPath)), "passport", "passport.png");
                response = await httpClient.PostAsync(url + "attachments/" + id, formData);
            }

            var newstudentAsJson = JsonConvert.SerializeObject(student);

            StringContent responseContent = new StringContent(newstudentAsJson, Encoding.UTF8, "application/json");

            response = await httpClient.PutAsync(url + id, responseContent);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var res2 = JsonConvert.DeserializeObject<Student>(content);

                var studentFromDb = await studentRepo.GetAsync(s => s.Id == id);
                if (studentFromDb != null)
                {
                    studentFromDb = student;
                    studentRepo.Update(studentFromDb);
                    await studentRepo.SaveAsyn();
                }
                return res2;
            }

            return null;
        }
    }
}
