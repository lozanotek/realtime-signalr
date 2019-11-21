using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MarkupSync.Services
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCorsFromConfig(this IServiceCollection services, string policyName)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();

            var originSetting = configuration["cors.Origins"];
            if(string.IsNullOrWhiteSpace(originSetting))
            {
                return services;
            }

            var origins = originSetting.Split(';', StringSplitOptions.RemoveEmptyEntries);
            
            services.AddCors(
                options => options.AddPolicy(policyName,
                    builder =>
                    {
                        builder
                            .WithOrigins(origins)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    })
            );

            return services;
        }
    }
}