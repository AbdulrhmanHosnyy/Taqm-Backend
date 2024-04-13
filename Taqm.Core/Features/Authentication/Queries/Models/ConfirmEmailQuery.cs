using MediatR;
using Taqm.Core.Bases;

namespace Taqm.Core.Features.Authentication.Queries.Models
{
    public record ConfirmEmailQuery(int UserId, string Code) : IRequest<Response<string>>;

}
