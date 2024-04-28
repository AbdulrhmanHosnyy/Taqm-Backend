using MediatR;
using Taqm.Core.Bases;

namespace Taqm.Core.Features.Users.Commands.Models
{
    public record ForgetPasswordCommand(string Email) : IRequest<Response<string>>;

}
