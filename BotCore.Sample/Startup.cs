using System.IO;
using Autofac;
using BotCore.EntityFrameworkCore.Extensions;
using BotCore.Extensions;
using BotCore.Models;
using Common;
using Data;
using Data.Repositories;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using Services;
using WebFramework.Configuration;
using WebFramework.CustomMapping;
using WebFramework.FirebaseInfrastructure.Extensions;
using WebFramework.FirebaseInfrastructure.Middleware;
using WebFramework.FirebaseInfrastructure.Models;
using WebFramework.FirebaseInfrastructure.Services;
using WebFramework.FirebaseInfrastructure.Utils;
using WebFramework.Middlewares;
using WebFramework.Model;
using WebFramework.Swagger;

namespace BotCore.Sample
{
    public class Startup
    {
        private readonly SiteSettings _siteSetting;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            _siteSetting = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }
        
        private AppSettings GetAppSettings()
        {
            var appSettings = new AppSettings();

            Configuration.Bind(nameof(AppSettings), appSettings);

            return appSettings;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")
                )
            );
            
            services.AddCors();

            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));

            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));

            services.AddSwagger(GetAppSettings());
            
            services.InitializeAutoMapper();

            //services.AddDbContext(Configuration);

            services.AddMinimalMvc();
            
            services.AddHttpClient();

            services.AddJwtAuthentication(_siteSetting.JwtSettings);
            
            services.AddFirebaseAdminWithCredentialFromFile("diplom-96ba7-firebase-adminsdk.json");

            services.AddCustomApiVersioning();
            
            services.AddScoped<IFirebaseAdminUtils, FirebaseAdminUtils>();
            
            services.AddScoped<IFirebaseService, FirebaseService>();

        }
        
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.AddServices();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.InitializeDatabase();
            
            app.UseCustomExceptionHandler();

            app.UseHsts(env);
            
            app.UseSwaggerAndUI();

            app.UseRouting();
            
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );
            
            app.UseHttpsRedirection();
            
            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseMiddleware<AuthorizationMiddleware>();

            app.UseEndpoints(
                endpoints => endpoints.MapControllers()
            );
        }
    }
}