using MediatR;
using Taqm.Core.Bases;

namespace Taqm.Core.Features.Authentication.Commands.Models
{
    public record ResetPasswordCommand(string Password, string ConfirmPassword, string Email, string Token) : IRequest<Response<string>>;
}
