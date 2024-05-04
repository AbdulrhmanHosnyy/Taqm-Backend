using MediatR;
using Taqm.Core.Bases;
using Taqm.Data.Responses;

namespace Taqm.Core.Features.Authorization.Claims.Queries.Models
{
    public record class ManageUserClaimsQuery(int UserId) : IRequest<Response<ManageUserClaimsResponse>>;
}
