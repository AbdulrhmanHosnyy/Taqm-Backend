using Microsoft.EntityFrameworkCore;
using Taqm.Data.Entities.Chat;
using Taqm.Infrastructure.Abstracts;
using Taqm.Infrastructure.Data;

namespace Taqm.Infrastructure.Repositories
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        #region Fields
        private readonly DbSet<Message> _messages;
        #endregion

        #region Constructors
        public MessageRepository(AppDbContext context) : base(context)
        {
            _messages = context.Set<Message>();
        }
        #endregion

        #region Methods

        #endregion
    }
}
