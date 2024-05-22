using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Taqm.Core.Bases;
using Taqm.Core.Features.Chats.Commands.Models;
using Taqm.Core.Resources;
using Taqm.Data.Entities.Chat;
using Taqm.Service.Abstracts;

namespace Taqm.Core.Features.Chats.Commands.Handlers
{
    public class ChatCommandHandler : ResponseHandler,
        IRequestHandler<SendMessageCommand, Response<string>>,
        IRequestHandler<UpdateMessageCommand, Response<string>>,
        IRequestHandler<DeleteMessageCommand, Response<string>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IChatService _chatService;
        private readonly IUserService _userService;
        #endregion

        #region Constructors
        public ChatCommandHandler(IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer,
            IChatService chatService, IUserService userService) : base(stringLocalizer)
        {
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _chatService = chatService;
            _userService = userService;
        }
        #endregion

        #region Handlers
        public async Task<Response<string>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            //  Getting the user 
            var user = await _userService.GetUserByIdAsync(request.UserId);
            if (user is null) NotFound<string>(_stringLocalizer[SharedResourcesKeys.SenderNotFound]);

            //  Getting the Recipient 
            var recipient = await _userService.GetUserByIdAsync(request.RecipientId);
            if (user is null) NotFound<string>(_stringLocalizer[SharedResourcesKeys.RecipientNotFound]);


            //  Mapping the message
            var message = _mapper.Map<Message>(request);

            var result = await _chatService.SendMessageAsync(user, recipient, message);

            if (result == "Failed") return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToSendMessage]);
            return Success((string)_stringLocalizer[SharedResourcesKeys.MessageSentSuccessfully]);
        }
        public async Task<Response<string>> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _chatService.GetByIdAsync(request.MessageId);
            var result = await _chatService.UpdateMessageAsync(message, request.MessageContent);
            if (result == "Failed") BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdate]);
            return Success((string)_stringLocalizer[SharedResourcesKeys.MessageUpdatedSuccessfully]);
        }
        public async Task<Response<string>> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _chatService.GetByIdAsync(request.MessageId);
            if (message is null) return NotFound<string>();

            var deleteResult = await _chatService.DeleteMessageAsync(message);

            if (deleteResult == "Failed") return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.DeleteFailed]);
            return Success((string)_stringLocalizer[SharedResourcesKeys.Deleted]);
        }
        #endregion
    }
}
