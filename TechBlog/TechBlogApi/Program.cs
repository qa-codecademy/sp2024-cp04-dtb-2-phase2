using Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
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
            var appSettings = builder.Configuration.GetSection("DbSettings");
            builder.Services.Configure<DatabaseSettings>(appSettings);
            DatabaseSettings dbSettings = appSettings.Get<DatabaseSettings>();
            var connectionString = dbSettings.ConnectionString;
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme // using microsoft.openapi.models
                {
                    Description = "Standard Authorisation header using the bearer scheme, e.g." +
                    "\bearer {token} \"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>(); // install swashbucke.aspnetcore.filters

            });
            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; // using Microsoft.AspNetCore.Authentication.JwtBearer;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters // using Microsoft.IdentityModel.Tokens;
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CORSPolicy", builder =>
                {
                    builder.AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials()
                           .SetIsOriginAllowed((hosts) => true);
                });
            });

            //builder.Services.

            DependencyInjectionHelper.InjectServices(builder.Services);
            DependencyInjectionHelper.InjectRepositories(builder.Services);

            //DependencyInjectionHelper.InjectDbContext(builder.Services, builder.Configuration.GetConnectionString("DefaultConnection"));
            DependencyInjectionHelper.InjectDbContext(builder.Services, builder.Configuration.GetSection("RenderConnString").Value);

            // Daniel's connection string: connectionString !!! !!!!!!
            //DependencyInjectionHelper.InjectDbContext(builder.Services, connectionString);

            //builder.Services.AddAuthentication(x =>
            //{
            //    //we will use JWT authentication
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(x =>
            //{
            //    //token configuration

            //    x.RequireHttpsMetadata = false;
            //    //we expect the token into the HttpContext
            //    x.SaveToken = true;
            //    //how to validate token
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateAudience = false,
            //        ValidateIssuer = false,
            //        ValidateIssuerSigningKey = true,
            //        //the secret key
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("Our very secret secretttt secret key"))
            //    };
            //});
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseCors("CORSPolicy");

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
