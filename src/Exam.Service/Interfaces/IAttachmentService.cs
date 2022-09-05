namespace Exam.Service.Interfaces
{
    public interface IAttachmentService
    {
        Task CreateAsync(long id, string IPath, string PPAth);

    }
}
