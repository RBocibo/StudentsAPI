using Students.Services.Interfaces;
using Students.Services.Services;

namespace Students.Web.Extensions.ServiceExtension
{
    public static class ServicesExtesndion
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
        }
    }
}
