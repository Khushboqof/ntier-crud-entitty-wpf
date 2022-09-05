using Exam.Domain.Entities.Students;
using Microsoft.EntityFrameworkCore;

namespace Exam.Data.Contexts
{
    public class ExamDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host = 127.0.0.1; Port = 5432; Database = Db; Username = postgres; Password = 2003");
        }
    }
}
