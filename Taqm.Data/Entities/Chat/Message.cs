using Taqm.Data.Enums;

namespace Taqm.Data.Entities.Chat
{
    public class Message
    {
        public int MessageId { get; set; }
        public string MessageContent { get; set; }
        public MessageTypeEnum MessageType { get; set; }
        public DateTime SentOn { get; set; } = DateTime.Now;
        public int ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; }
    }
}
