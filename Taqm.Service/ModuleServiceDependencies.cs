using Microsoft.Extensions.DependencyInjection;
using Taqm.Service.Abstracts;
using Taqm.Service.Services;

namespace Taqm.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddTransient<IChatService, ChatService>();


            return services;
        }
    }
}
