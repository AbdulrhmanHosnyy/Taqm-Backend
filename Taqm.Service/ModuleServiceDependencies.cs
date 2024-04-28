using Microsoft.Extensions.DependencyInjection;
using Taqm.Service.Abstracts;
using Taqm.Service.Services;

namespace Taqm.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IPostureService, PostureService>();
            services.AddTransient<IEmailService, EmailService>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IFileService, FileService>();


            return services;
        }
    }
}
