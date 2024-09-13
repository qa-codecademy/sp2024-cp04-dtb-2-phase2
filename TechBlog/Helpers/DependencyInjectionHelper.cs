using Data_Access;
using Data_Access.Implementations;
using Data_Access.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Implementation;
using Services.Interfaces;

namespace Helpers
{
    public class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TechBlogDbContext>(x => x.UseSqlServer(connectionString));
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();

        }

        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddTransient<IUserReposiotry, UserRepository>();
        }
    }
}
