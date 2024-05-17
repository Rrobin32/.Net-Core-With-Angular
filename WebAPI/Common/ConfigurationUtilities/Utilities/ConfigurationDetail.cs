using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.IdentityModel.Tokens;


using ConfigurationUtilities.Settings;

namespace ConfigurationUtilities.Utilities
{
    public static class ConfigurationDetail
    {
        public static void Configure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {

            if (configuration["ConnectionStrings:DBConnect"] != null)
            {

                if (configuration.GetValue<bool>("AppSettings:IsConnectStringEncrypt"))
                {
                    ConnectionStrings.DBConnect = EncryptionDecryption.DecryptStringAES(configuration["ConnectionStrings:DBConnect"].ToString());
                }
                else
                {
                    ConnectionStrings.DBConnect = configuration["ConnectionStrings:DBConnect"].ToString();
                }
            
            }
            if (configuration["ConnectionStrings:Database"] != null)
            {
                ConnectionStrings.Database = configuration["ConnectionStrings:Database"].ToString();
            }

            if (configuration["JWT:Secret"] != null)
            {
                JWT.Secret = configuration["JWT:Secret"];
            }
            JWT.ValidIssuer = configuration["JWT:ValidIssuer"];
            JWT.ValidAudience = configuration["JWT:ValidAudience"];
            JWT.ExpireTime = configuration["JWT:ExpireTime"];

            AppSettings.ContentRootPath = hostEnvironment.ContentRootPath;
            AppSettings.EnvironmentName = hostEnvironment.EnvironmentName;

            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            services.Configure<ValidationMessages>(configuration.GetSection("ValidationMessages"));

            // If using Kestrel:
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // If using IIS:
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });



            //ADD Swagger Configuration
            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = configuration["JWT:ValidIssuer"],
                   ValidAudience = configuration["JWT:ValidAudience"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
               };
           });
        }
    }
}
