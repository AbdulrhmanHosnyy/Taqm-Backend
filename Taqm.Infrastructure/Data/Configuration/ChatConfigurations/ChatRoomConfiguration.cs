using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taqm.Data.Entities.Chat;

namespace Taqm.Infrastructure.Data.Configuration.ChatConfigurations
{
    public class ChatRoomConfiguration : IEntityTypeConfiguration<ChatRoom>
    {
        public void Configure(EntityTypeBuilder<ChatRoom> builder)
        {
            builder.HasKey(c => c.ChatRoomId);
        }
    }
}
