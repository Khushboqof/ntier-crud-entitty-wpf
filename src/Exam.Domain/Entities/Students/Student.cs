using Exam.Domain.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exam.Domain.Entities.Students
{
    public class Student : Auditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Faculty { get; set; }

        public long? PassportId { get; set; }

        [ForeignKey("PassportId")]
        public Attachment Passport { get; set; }

        public long? ImageId { get; set; }

        [ForeignKey("ImageId")]
        public Attachment Image { get; set; }
    }
}
