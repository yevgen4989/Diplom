using Common;
using Common.Exceptions;
using Common.Utilities;
using Data;
using Data.Repositories;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Owin.Security.OAuth;
using WebFramework.Model;

namespace WebFramework.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                //.ConfigureWarnings(warning => warning.Throw(RelationalEventId.QueryClientEvaluationWarning));
            });
        }

        public static void AddMinimalMvc(this IServiceCollection services)
        {
            services.AddControllers(options =>
                {
                options.Filters.Add(new AuthorizeFilter());
                
                // options.Filters.Add(new AuthorizeFilter(OAuthDefaults.AuthenticationType));

                //options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                //options.UseYeKeModelBinder();
                
                
            })
                .AddNewtonsoftJson(option =>
                {
                option.SerializerSettings.Converters.Add(new StringEnumConverter());
                option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //option.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                //option.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });
            
            
            
            services.AddSwaggerGenNewtonsoftSupport();
        }
        
        public static void AddElmahCore(this IServiceCollection services, IConfiguration configuration, SiteSettings siteSetting)
        {
            services.AddElmah<SqlErrorLog>(options =>
            {
                options.Path = siteSetting.ElmahPath;
                options.ConnectionString = configuration.GetConnectionString("Elmah");
                //options.CheckPermissionAction = httpContext => httpContext.User.Identity.IsAuthenticated;
            });
        }

        public static void AddJwtAuthentication(this IServiceCollection services, JwtSettings jwtSettings)
        {
            services.AddAuthentication(
                options => {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            )
                .AddJwtBearer(options => {
                    options.Authority = "https://securetoken.google.com/diplom-96ba7";
                    options.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuer = true,
                        ValidIssuer = "https://securetoken.google.com/diplom-96ba7",
                        ValidateAudience = true,
                        ValidAudience = "diplom-96ba7",
                        ValidateLifetime = true
                    };
                    options.Events = new JwtBearerEvents {
                        OnAuthenticationFailed = context => {
                            if (context.Exception != null)
                                throw new AppException(ApiResultStatusCode.UnAuthorized, "Authentication failed.", HttpStatusCode.Unauthorized, context.Exception, null);
                    
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = async context => {
                            var userRepository = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();

                            if (context.Principal.Identity is ClaimsIdentity identity)
                            {

                                ClaimsIdentity identity_2 = identity;
                                
                                FirebaseInfrastructure.Models.ClaimsIdentity? claimsIdentity = 
                                    JsonConvert.DeserializeObject<FirebaseInfrastructure.Models.ClaimsIdentity>(
                                        identity.Claims.First(x => x.Type == "firebase").Value
                                    );

                                if (userRepository.IsExistByFirebaseId(identity.Claims.First(x => x.Type == "user_id").Value)) {
                                    
                                    UserOption? userOption = await userRepository.GetByFirebaseIdAsync(identity.Claims.First(x => x.Type == "user_id").Value, context.HttpContext.RequestAborted);
                                    
                                    identity.AddClaim(new System.Security.Claims.Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(userOption)));
                                }
                                else
                                {
                                    UserOption? userOption = new UserOption
                                    {
                                        Email = claimsIdentity.Identities.email[(claimsIdentity.Identities.email.Count - 1)],
                                        FirebaseId = identity.Claims.First(x => x.Type == "user_id").Value,
                                        LastLoginDate = DateTimeOffset.FromUnixTimeSeconds(Int64.Parse(identity.Claims.First(x => x.Type == "auth_time").Value)),
                                    };

                                    await userRepository.AddAsync(userOption, context.HttpContext.RequestAborted);

                                    identity.AddClaim(new System.Security.Claims.Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(userOption)));
                                }
                                
                                
                            }
                        }
                    };
                });
        }

        public static void AddCustomApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                //url segment => {version}
                options.AssumeDefaultVersionWhenUnspecified = true; //default => false;
                options.DefaultApiVersion = new ApiVersion(1, 0); //v1.0 == v1
                options.ReportApiVersions = true;

                //ApiVersion.TryParse("1.0", out var version10);
                //ApiVersion.TryParse("1", out var version1);
                //var a = version10 == version1;

                //options.ApiVersionReader = new QueryStringApiVersionReader("api-version");
                // api/posts?api-version=1

                //options.ApiVersionReader = new UrlSegmentApiVersionReader();
                // api/v1/posts

                //options.ApiVersionReader = new HeaderApiVersionReader(new[] { "Api-Version" });
                // header => Api-Version : 1

                //options.ApiVersionReader = new MediaTypeApiVersionReader()

                //options.ApiVersionReader = ApiVersionReader.Combine(new QueryStringApiVersionReader("api-version"), new UrlSegmentApiVersionReader())
                // combine of [querystring] & [urlsegment]
            });
        }
    }
}
