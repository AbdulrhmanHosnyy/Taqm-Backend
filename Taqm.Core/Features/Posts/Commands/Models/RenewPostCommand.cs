using MediatR;
using Taqm.Core.Bases;

namespace Taqm.Core.Features.Posts.Commands.Models
{
    public record RenewPostCommand(int Id) : IRequest<Response<string>>;
}
