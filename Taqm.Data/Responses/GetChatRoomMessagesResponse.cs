using Taqm.Data.Enums;

namespace Taqm.Data.Responses
{
    public class GetChatRoomMessagesResponse
    {
        public int MessageId { get; set; }
        public string MessageContent { get; set; }
        public MessageTypeEnum MessageType { get; set; }
        public DateTime SentOn { get; set; } = DateTime.Now;
    }
}
