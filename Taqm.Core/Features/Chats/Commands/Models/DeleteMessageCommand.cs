using MediatR;
using Taqm.Core.Bases;

namespace Taqm.Core.Features.Chats.Commands.Models
{
    public record DeleteMessageCommand(int MessageId) : IRequest<Response<string>>;
}
