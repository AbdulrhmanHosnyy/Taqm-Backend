using System.Data.Entity;
using System.Linq.Expressions;
using Taqm.Data.Consts;
using Taqm.Data.Entities.Chat;
using Taqm.Data.Entities.Identity;
using Taqm.Data.Responses;
using Taqm.Infrastructure.Abstracts;
using Taqm.Infrastructure.Data;
using Taqm.Service.Abstracts;

namespace Taqm.Service.Services
{
    public class ChatService : IChatService
    {
        #region Fields
        private readonly IMessageRepository _messageRepository;
        private readonly IChatRoomRepository _chatRoomRepository;
        private readonly IUserChatRoomRepository _userChatRoomRepository;
        private readonly AppDbContext _appDbContext;
        #endregion

        #region Constructors
        public ChatService(IMessageRepository messageRepository, IChatRoomRepository chatRoomRepository,
            IUserChatRoomRepository userChatRoomRepository, AppDbContext appDbContext)
        {
            _messageRepository = messageRepository;
            _chatRoomRepository = chatRoomRepository;
            _userChatRoomRepository = userChatRoomRepository;
            _appDbContext = appDbContext;
        }

        #endregion

        #region Methods
        public async Task<Message> GetByIdAsync(int id) => await _messageRepository.GetByIdAsync(id);
        public async Task<string> SendMessageAsync(User user, User recipient, Message message)
        {
            var transaction = await _appDbContext.Database.BeginTransactionAsync();
            try
            {
                //  Check if there is a chatroom
                var chatRoom = await _userChatRoomRepository.GetChatRoomAsync(user, recipient);

                if (chatRoom is null)
                {
                    //  Creating a new chat room and add it to the database
                    chatRoom = new ChatRoom();
                    await _chatRoomRepository.CreateAsync(chatRoom);

                    //  Adding the users to the chatroom and updating the dataabse
                    var userChatRoomSender = new UserChatRoom { User = user, ChatRoom = chatRoom };
                    var userChatRoomRecipient = new UserChatRoom { User = recipient, ChatRoom = chatRoom };

                    var chatRoomMembers = new List<UserChatRoom>
                    {
                        userChatRoomSender,
                        userChatRoomRecipient
                    };
                    await _userChatRoomRepository.CreateRangeAsync(chatRoomMembers);
                }

                //  Adding the message to the chatRoom
                message.ChatRoom = chatRoom;
                await _messageRepository.CreateAsync(message);

                //  Commit transactions
                await transaction.CommitAsync();
                return "Success";
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return "Failed";
            }
        }
        public async Task<string> UpdateMessageAsync(Message message, string newContent)
        {
            try
            {
                message.MessageContent = newContent;
                await _messageRepository.UpdateAsync(message);
                return "Success";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }
        public async Task<string> DeleteMessageAsync(Message message)
        {
            var result = _messageRepository.DeleteAsync(message);

            return result != null ? "Success" : "Failed";
        }
        private Expression<Func<ChatRoom, bool>> FilterChatRoomExpression(int userId) => cr => cr.UserChatRooms.Any(u => u.UserId == userId);
        private Expression<Func<ChatRoom, object>> OrderChatRoomByExpression() => cr => cr.ChatRoomId;
        private Expression<Func<ChatRoom, GetUserChatRoomsResponse>> MapChatRoomExpression(int userId)
        {
            return cr => new GetUserChatRoomsResponse
            {
                ChatRoomId = cr.ChatRoomId,
                CorrespondentFullName = $"{cr.UserChatRooms.FirstOrDefault(u => u.UserId != userId).User.FirstName} {cr.UserChatRooms.FirstOrDefault(u => u.UserId != userId).User.LastName}",
                LastMessage = cr.Messages.Any() ? cr.Messages.OrderByDescending(m => m.SentOn).First().MessageContent : "No message yet",
                LastMessageTime = cr.Messages.Any() ? cr.Messages.OrderByDescending(m => m.SentOn).First().SentOn : DateTime.MinValue,
            };
        }
        public IQueryable<GetUserChatRoomsResponse> GetUserChatRoomsAsQuerable(int userId)
        {
            //  Getting the chatrooms as querable
            var chatRoomAsQuerable = _chatRoomRepository.GetTableNoTracking().Include(cr => cr.Messages).Include(cr => cr.UserChatRooms);

            //  Filtering the chatrooms by the userId
            Expression<Func<ChatRoom, bool>> userChatroomsFilterExpression = FilterChatRoomExpression(userId);
            var orderByExpression = OrderChatRoomByExpression();
            var filteredUserChatrooms = _chatRoomRepository
                .FindAllAsQuerable(chatRoomAsQuerable, userChatroomsFilterExpression, orderByExpression, OrderBy.Descending);

            //  Map the chatroom
            Expression<Func<ChatRoom, GetUserChatRoomsResponse>> userChatroomsMappingExpression = MapChatRoomExpression(userId);
            var mappedUserChatRooms = filteredUserChatrooms.Select(userChatroomsMappingExpression);

            //  Returning the mapped list
            return mappedUserChatRooms;
        }
        private Expression<Func<Message, bool>> FilterMessagesExpression(int chatRoomId) => m => m.ChatRoomId.Equals(chatRoomId);
        private Expression<Func<Message, GetChatRoomMessagesResponse>> MapChatRoomMessagesExpression()
        {
            return m => new GetChatRoomMessagesResponse
            {
                MessageId = m.MessageId,
                MessageContent = m.MessageContent,
                MessageType = m.MessageType,
                SentOn = m.SentOn,
            };
        }
        public IQueryable<GetChatRoomMessagesResponse> GetChatRoomMessagesAsQuerable(int chatRoomId)
        {
            //  Getting the messages as querable
            var messageAsQuerable = _messageRepository.GetTableNoTracking();

            //  Filtering the messages by the ChatRoomId
            Expression<Func<Message, bool>> messageFilterExpression = FilterMessagesExpression(chatRoomId);
            var filteredMessages = _messageRepository.FindAllAsQuerable(messageAsQuerable, messageFilterExpression);

            //  Mapping the messages 
            Expression<Func<Message, GetChatRoomMessagesResponse>> chatRoomMessagesExpression = MapChatRoomMessagesExpression();
            var mappedMessages = filteredMessages.Select(chatRoomMessagesExpression);

            return mappedMessages;
        }
        #endregion

    }
}
