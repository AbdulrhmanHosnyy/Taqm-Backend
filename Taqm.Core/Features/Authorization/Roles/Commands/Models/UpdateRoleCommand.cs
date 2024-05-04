using MediatR;
using Taqm.Core.Bases;
using Taqm.Data.Requests;

namespace Taqm.Core.Features.Authorization.Roles.Commands.Models
{
    public record UpdateRoleCommand(int Id, string Name) : UpdateRoleRequest(Id, Name), IRequest<Response<string>>;
}
