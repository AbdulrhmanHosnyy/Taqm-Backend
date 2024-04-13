using MediatR;
using Taqm.Core.Bases;

namespace Taqm.Core.Features.Users.Commands.Models
{
    public record DeleteUserCommand(int Id) : IRequest<Response<string>>;
}
