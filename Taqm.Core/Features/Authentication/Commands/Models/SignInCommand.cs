using MediatR;
using Taqm.Core.Bases;
using Taqm.Data.Responses;

namespace Taqm.Core.Features.Authentication.Commands.Models
{
    public record SignInCommand(string Email, string Password) : IRequest<Response<JwtAuthResponse>>;

}
