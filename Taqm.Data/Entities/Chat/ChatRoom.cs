namespace Taqm.Data.Entities.Chat
{
    public class ChatRoom
    {
        public ChatRoom()
        {
            Messages = new HashSet<Message>();
            UserChatRooms = new HashSet<UserChatRoom>();
        }
        public int ChatRoomId { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<UserChatRoom> UserChatRooms { get; set; }
    }
}
