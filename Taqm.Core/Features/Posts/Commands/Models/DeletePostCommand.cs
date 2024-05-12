using MediatR;
using Taqm.Core.Bases;

namespace Taqm.Core.Features.Posts.Commands.Models
{
    public record DeletePostCommand(int Id) : IRequest<Response<string>>;
}
