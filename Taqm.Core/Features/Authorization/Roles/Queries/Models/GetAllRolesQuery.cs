using MediatR;
using Taqm.Core.Bases;
using Taqm.Core.Features.Authorization.Roles.Queries.Responses;

namespace Taqm.Core.Features.Authorization.Roles.Queries.Models
{
    public record GetAllRolesQuery() : IRequest<Response<List<GetRoleResponse>>>;
}
