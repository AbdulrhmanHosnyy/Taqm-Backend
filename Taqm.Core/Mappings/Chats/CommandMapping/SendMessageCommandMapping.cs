using Taqm.Core.Features.Chats.Commands.Models;
using Taqm.Data.Entities.Chat;

namespace Taqm.Core.Mappings.Chats
{
    public partial class ChatProfile
    {
        public void SendMessageCommandMapping()
        {
            CreateMap<SendMessageCommand, Message>()
                .ForMember(dest => dest.ChatRoomId, opt => opt.Ignore())
                .ForMember(dest => dest.ChatRoom, opt => opt.Ignore());
        }
    }
}
