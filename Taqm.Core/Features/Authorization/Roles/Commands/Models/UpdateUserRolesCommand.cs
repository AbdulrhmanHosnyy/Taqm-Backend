using MediatR;
using Taqm.Core.Bases;
using Taqm.Data.Requests;

namespace Taqm.Core.Features.Authorization.Roles.Commands.Models
{
    public record UpdateUserRolesCommand(int Id, List<string> RolesNames) : UpdateUserRolesRequest(Id, RolesNames), IRequest<Response<string>>;

}
