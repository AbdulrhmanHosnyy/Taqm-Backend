using MediatR;
using Taqm.Core.Bases;
using Taqm.Data.Responses;

namespace Taqm.Core.Features.Authorization.Roles.Queries.Models
{
    public record ManageUserRolesQuery(int UserId) : IRequest<Response<ManageUserRolesResponse>>;
}
