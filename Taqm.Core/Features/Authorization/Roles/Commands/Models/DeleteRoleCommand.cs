using MediatR;
using Taqm.Core.Bases;

namespace Taqm.Core.Features.Authorization.Roles.Commands.Models
{
    public record DeleteRoleCommand(int Id) : IRequest<Response<string>>;
}
