using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Taqm.Core.Bases;
using Taqm.Core.Features.Chats.Queries.Models;
using Taqm.Core.Resources;
using Taqm.Core.Wrappers;
using Taqm.Data.Responses;
using Taqm.Service.Abstracts;

namespace Taqm.Core.Features.Chats.Queries.Handlers
{
    public class ChatQueryHandler : ResponseHandler,
        IRequestHandler<GetUserPaginatedChatRoomsQuery, PaginatedResult<GetUserChatRoomsResponse>>,
        IRequestHandler<GetChatRoomMessagesQuery, PaginatedResult<GetChatRoomMessagesResponse>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IChatService _chatService;
        private readonly IUserService _userService;
        #endregion

        #region Constructors
        public ChatQueryHandler(IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer,
            IChatService chatService, IUserService userService) : base(stringLocalizer)
        {
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _chatService = chatService;
            _userService = userService;
        }
        #endregion

        #region Handlers
        public async Task<PaginatedResult<GetUserChatRoomsResponse>> Handle(GetUserPaginatedChatRoomsQuery request, CancellationToken cancellationToken)
        {
            //  Get user chatrooms
            var userChatRooms = _chatService.GetUserChatRoomsAsQuerable(request.UserId);

            //  Paginate the chatrooms
            var paginatedUserChatRooms = await userChatRooms.ToPaginatedListAsync(request.PageNumber, request.PageSize);

            //  Getting the count of data returned
            paginatedUserChatRooms.Meta = new { Count = paginatedUserChatRooms.Data.Count() };

            return paginatedUserChatRooms;
        }
        public async Task<PaginatedResult<GetChatRoomMessagesResponse>> Handle(GetChatRoomMessagesQuery request, CancellationToken cancellationToken)
        {
            var chatRoomMessages = _chatService.GetChatRoomMessagesAsQuerable(request.ChatRoomId);
            var paginatedChatRoomMessages = await chatRoomMessages.ToPaginatedListAsync(request.PageNumber, request.PageSize);
            paginatedChatRoomMessages.Meta = new { Count = paginatedChatRoomMessages.Data.Count() };
            return paginatedChatRoomMessages;
        }
        #endregion
    }
}
