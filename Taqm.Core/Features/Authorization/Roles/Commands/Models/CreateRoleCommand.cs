using MediatR;
using Taqm.Core.Bases;

namespace Taqm.Core.Features.Authorization.Roles.Commands.Models
{
    public record CreateRoleCommand(string Name) : IRequest<Response<string>>;
}
