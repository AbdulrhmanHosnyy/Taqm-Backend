using MediatR;
using Taqm.Core.Bases;

namespace Taqm.Core.Features.Authentication.Queries.Models
{
    public record ResetPasswordTokenQuery(string token) : IRequest<Response<string>>;
}
