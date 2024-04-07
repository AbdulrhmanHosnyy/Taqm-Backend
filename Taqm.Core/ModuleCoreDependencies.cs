using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Taqm.Core.Behaviours;


namespace Taqm.Core
{
    public static class ModuleCoreDependencies
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
        {
            //  Mediator Configuration
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            //  Automapper Configuration    
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //  Get Validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //  Validators Configuration
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
