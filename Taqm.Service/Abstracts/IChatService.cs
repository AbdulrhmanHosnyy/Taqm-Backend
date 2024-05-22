using Taqm.Data.Entities.Chat;
using Taqm.Data.Entities.Identity;
using Taqm.Data.Responses;

namespace Taqm.Service.Abstracts
{
    public interface IChatService
    {
        Task<string> SendMessageAsync(User user, User recipient, Message message);
        public Task<Message> GetByIdAsync(int id);
        public Task<string> UpdateMessageAsync(Message message, string newContent);
        public Task<string> DeleteMessageAsync(Message message);
        public IQueryable<GetUserChatRoomsResponse> GetUserChatRoomsAsQuerable(int userId);
        public IQueryable<GetChatRoomMessagesResponse> GetChatRoomMessagesAsQuerable(int chatRoomId);
    }
}
