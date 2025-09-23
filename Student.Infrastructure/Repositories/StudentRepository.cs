using Students.Domain.Interfaces;
using Students.Infrastructure.DBContext;

namespace Students.Infrastructure.Repositories
{
    public class StudentRepository : BaseRepository<Domain.Entities.Student>, IStudentRepository
    {
        public StudentRepository(StudentDatabaseContext context) : base(context)
        {
        }
    }
}
