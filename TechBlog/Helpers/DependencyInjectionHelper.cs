using Data_Access;
using Data_Access.Implementations;
using Data_Access.Interfaces;
using Mappers.MapperConfig;
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
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IStarService, StarService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<ITokenHelper, TokenHelper>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        }

        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddTransient<INewsLetterRepository, NewsLetterRepository>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IStarRepository, StarRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IImageRepository, ImageRepository>();
        }
    }
}
