using Microsoft.EntityFrameworkCore;

namespace Students.Infrastructure.DBContext
{
    public class StudentDatabaseContext : DbContext
    {
        public StudentDatabaseContext(DbContextOptions<StudentDatabaseContext> dbContext)
            : base(dbContext)
        {

        }

        public DbSet<Students.Domain.Entities.Student> Students { get; set; }
    }
}
