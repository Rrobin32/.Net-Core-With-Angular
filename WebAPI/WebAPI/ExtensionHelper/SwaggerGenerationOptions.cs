using Microsoft.OpenApi.Models;
using System.Reflection;

namespace WebAPI.ExtensionHelper
{
    public static class SwaggerGenerationOptions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,$"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "JNKA Voice API Secured by JWT Token",
                    Version = "v1"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Bearer Authentication with JWT Token",
                    Type = SecuritySchemeType.Http
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                              {
                                  Id = "Bearer",
                                  Type = ReferenceType.SecurityScheme
                              }
                        },
                        new List<string>()
                    }
                });
            });
            return services;
        }
    }
}
