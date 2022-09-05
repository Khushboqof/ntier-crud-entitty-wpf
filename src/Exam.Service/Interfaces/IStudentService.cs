using Exam.Domain.Entities.Students;
using Exam.Service.DTOs.StudenDto;

namespace Exam.Service.Interfaces
{
    public interface IStudentService
    {
        Task<Student> CreateAsync(StudentForCcreation student);
        Task<bool> DeleteAsync(long id);
        Task<Student> GetAsync(long id);
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> UpdateAsync(long id, Student student, string portraitPath, string passportPath);
    }
}
