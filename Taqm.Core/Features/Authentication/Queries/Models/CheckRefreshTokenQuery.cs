using MediatR;
using Taqm.Core.Bases;
using Taqm.Data.Responses;

namespace Taqm.Core.Features.Authentication.Queries.Models
{
    public record CheckRefreshTokenQuery(string RefreshToken) : IRequest<Response<JwtAuthResponse>>;

}
