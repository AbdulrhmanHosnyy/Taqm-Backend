using AutoMapper;

namespace Taqm.Core.Mappings.Chats
{
    public partial class ChatProfile : Profile
    {
        public ChatProfile()
        {
            SendMessageCommandMapping();
        }
    }
}
