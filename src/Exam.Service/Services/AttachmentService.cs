using Exam.Data.IRepositories;
using Exam.Data.Repositories;
using Exam.Service.Constants;
using Exam.Service.Interfaces;

namespace Exam.Service.Services
{
    internal class AttachmentService : IAttachmentService
    {
        private readonly string Url = ApiConst.Base_Url;
        private readonly IAttachmentRepository attachmentRepository;

        public AttachmentService()
        {
            attachmentRepository = new AttachmentRepository();
        }

        public async Task CreateAsync(long id, string imagePath, string passportPath)
        {
            HttpClient client = new HttpClient();

            MultipartFormDataContent formData = new MultipartFormDataContent();
            if (imagePath is not null)
                formData.Add(new StreamContent(File.OpenRead(imagePath)), "image", "image.png");

            if (passportPath is not null)
                formData.Add(new StreamContent(File.OpenRead(passportPath)), "passport", "passport.png");

            HttpResponseMessage response = await client.PostAsync(Url + "Attachments/" + id, formData);

            await response.Content.ReadAsStringAsync();
        }
    }
}
