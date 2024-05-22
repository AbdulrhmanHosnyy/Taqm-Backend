using Microsoft.EntityFrameworkCore;
using Taqm.Data.Entities.Chat;
using Taqm.Infrastructure.Abstracts;
using Taqm.Infrastructure.Data;

namespace Taqm.Infrastructure.Repositories
{
    public class ChatRoomRepository : GenericRepository<ChatRoom>, IChatRoomRepository
    {
        #region Fields
        private readonly DbSet<ChatRoom> _chatRooms;
        #endregion

        #region Constructors
        public ChatRoomRepository(AppDbContext context) : base(context)
        {
            _chatRooms = context.Set<ChatRoom>();
        }
        #endregion

        #region Methods

        #endregion
    }
}
