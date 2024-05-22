using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taqm.Data.Entities.Chat;

namespace Taqm.Infrastructure.Data.Configuration.ChatConfigurations
{
    public class UserChatRoomConfiguration : IEntityTypeConfiguration<UserChatRoom>
    {
        public void Configure(EntityTypeBuilder<UserChatRoom> builder)
        {
            builder.HasKey(uc => new { uc.UserId, uc.ChatRoomId });

            builder.HasOne(uc => uc.User)
                   .WithMany(uc => uc.UserChatRooms)
                   .HasForeignKey(uc => uc.UserId);

            builder.HasOne(uc => uc.ChatRoom)
                   .WithMany(uc => uc.UserChatRooms)
                   .HasForeignKey(uc => uc.ChatRoomId);
        }
    }
}
