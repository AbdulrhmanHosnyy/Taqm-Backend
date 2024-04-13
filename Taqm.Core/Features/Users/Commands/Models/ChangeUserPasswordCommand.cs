using MediatR;
using Taqm.Core.Bases;

namespace Taqm.Core.Features.Users.Commands.Models
{
    public record ChangeUserPasswordCommand(int Id, string CurrentPassword, string NewPassword, string ConfirmPassword)
        : IRequest<Response<string>>;
}
