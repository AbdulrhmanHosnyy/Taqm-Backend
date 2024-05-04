using MediatR;
using Taqm.Core.Bases;
using Taqm.Data.Requests;

namespace Taqm.Core.Features.Authorization.Claims.Commands.Models
{
    public record UpdateUserClaimsCommand(int Id, List<string> ClaimsNames) : UpdateUserClaimsRequest(Id, ClaimsNames), IRequest<Response<string>>;

}
