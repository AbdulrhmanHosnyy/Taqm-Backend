using MediatR;
using Taqm.Core.Bases;
using Taqm.Data.Enums;

namespace Taqm.Core.Features.Chats.Commands.Models
{
    public record SendMessageCommand(int UserId, int RecipientId, string MessageContent, MessageTypeEnum MessageType)
        : IRequest<Response<string>>;
}
