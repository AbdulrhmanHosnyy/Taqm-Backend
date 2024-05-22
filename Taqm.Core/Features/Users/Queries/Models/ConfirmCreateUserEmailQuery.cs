using MediatR;
using Taqm.Core.Bases;

namespace Taqm.Core.Features.Users.Queries.Models
{
    public record ConfirmCreateUserEmailQuery(int UserId, string EmailConfirmationToken) : IRequest<Response<string>>;
}
