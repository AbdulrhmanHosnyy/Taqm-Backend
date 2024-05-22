using Microsoft.Extensions.DependencyInjection;
using Taqm.Infrastructure.Abstracts;
using Taqm.Infrastructure.Repositories;

namespace Taqm.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IChatRoomRepository, ChatRoomRepository>();
            services.AddTransient<IUserChatRoomRepository, UserChatRoomRepository>();

            return services;
        }
    }
}
