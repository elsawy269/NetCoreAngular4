using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MessageBoard.BackEnd.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MessageBoard.BackEnd
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            var signinKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("THIS IS SECERECT THREE"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey=signinKey,
                        ValidateLifetime=false,
                        ValidateIssuer=false,
                        ValidateAudience=false
                    };
                });
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase());
            services.AddCors(option => option.AddPolicy("Cors", builder =>
            {
                builder
                 .AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader();
            }));



            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseAuthentication();

            app.UseCors("Cors");
            app.UseMvc();
            SeedingData(app.ApplicationServices.GetService<ApiContext>());
        }

        public void SeedingData(ApiContext context)
        {
            context.messages.Add(new Message { Text = "Message text", Owner = "elsawy" });
            context.messages.Add(new Message { Text = "Other text", Owner = "mhmd" });
            context.messages.Add(new Message { Text = "Other text", Owner = "sa3d" });
            context.Users.Add(new User { Email = "a", FirstName = "Elsawy", Password = "a",Id="1" });
            context.SaveChanges();
        }
    }
}
