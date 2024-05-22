using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taqm.Data.Entities.Chat;

namespace Taqm.Infrastructure.Data.Configuration.ChatConfigurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m => m.MessageId);

            builder.HasOne(m => m.ChatRoom)
                   .WithMany(m => m.Messages)
                   .HasForeignKey(uc => uc.ChatRoomId);
        }
    }
}
