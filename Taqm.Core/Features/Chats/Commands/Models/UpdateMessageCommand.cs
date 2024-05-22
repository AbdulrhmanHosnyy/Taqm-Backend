using MediatR;
using Taqm.Core.Bases;

namespace Taqm.Core.Features.Chats.Commands.Models
{
    public record UpdateMessageCommand(int MessageId, string MessageContent) : IRequest<Response<string>>;
}
