using Students.Domain.Generic;
using Students.Infrastructure.DBContext;

namespace Students.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudentDatabaseContext _dbContext;

        public UnitOfWork(StudentDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
