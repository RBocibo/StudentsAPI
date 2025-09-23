using Microsoft.EntityFrameworkCore;
using Students.Infrastructure.DBContext;

namespace Students.Web.Extensions.DatabaseExtension
{
    public static class DatabaseExtension
    {
        public static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StudentDatabaseContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("StudentDbConnection"));
            });
        }
    }
}
