namespace Taqm.Data.Responses
{
    public class GetUserChatRoomsResponse
    {
        public int ChatRoomId { get; set; }
        public string CorrespondentFullName { get; set; }
        public string LastMessage { get; set; }
        public DateTime LastMessageTime { get; set; }
    }
}
