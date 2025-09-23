using Students.Domain.Generic;
using Students.Domain.Interfaces;
using Students.Infrastructure.Repositories;

namespace Students.Web.Extensions.RepositoryExtenstion
{
    public static class RepositoryExtenstions
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
