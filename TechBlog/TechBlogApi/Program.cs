using Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TechBlogApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            DependencyInjectionHelper.InjectServices(builder.Services);
            DependencyInjectionHelper.InjectRepositories(builder.Services);

            var appSettings = builder.Configuration.GetSection("DbSettings");
            builder.Services.Configure<DatabaseSettings>(appSettings);
            DatabaseSettings dbSettings = appSettings.Get<DatabaseSettings>();
            var connectionString = dbSettings.ConnectionString;

            DependencyInjectionHelper.InjectDbContext(builder.Services, connectionString);

            builder.Services.AddAuthentication(x =>
            {
                //we will use JWT authentication
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                //token configuration

                x.RequireHttpsMetadata = false;
                //we expect the token into the HttpContext
                x.SaveToken = true;
                //how to validate token
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    //the secret key
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("Our very secret secretttt secret key"))
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
