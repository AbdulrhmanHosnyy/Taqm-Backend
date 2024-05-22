using Taqm.Data.Entities.Chat;
using Taqm.Data.Entities.Identity;

namespace Taqm.Infrastructure.Abstracts
{
    public interface IUserChatRoomRepository : IGenericRepository<UserChatRoom>
    {
        public Task<ChatRoom> GetChatRoomAsync(User sender, User recipient);
    }
}
