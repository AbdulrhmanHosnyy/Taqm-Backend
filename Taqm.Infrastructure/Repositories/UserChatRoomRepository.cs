using Microsoft.EntityFrameworkCore;
using Taqm.Data.Entities.Chat;
using Taqm.Data.Entities.Identity;
using Taqm.Infrastructure.Abstracts;
using Taqm.Infrastructure.Data;

namespace Taqm.Infrastructure.Repositories
{
    public class UserChatRoomRepository : GenericRepository<UserChatRoom>, IUserChatRoomRepository
    {
        #region Fields
        private readonly DbSet<UserChatRoom> _userChatRomms;
        #endregion

        #region Constructors
        public UserChatRoomRepository(AppDbContext context) : base(context)
        {
            _userChatRomms = context.Set<UserChatRoom>();
        }
        #endregion

        #region Methods
        public async Task<ChatRoom> GetChatRoomAsync(User sender, User recipient)
        {
            return await _userChatRomms.Where(uc => uc.ChatRoom.UserChatRooms.Any(ur => ur.User == sender)
                                && uc.ChatRoom.UserChatRooms.Any(ur => ur.User == recipient))
                          .Select(uc => uc.ChatRoom)
                          .FirstOrDefaultAsync();
        }
        #endregion
    }
}
