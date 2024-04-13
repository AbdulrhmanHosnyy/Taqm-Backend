using MediatR;
using Taqm.Core.Bases;

namespace Taqm.Core.Features.Users.Commands.Models
{
    public record CreateUserCommand(string Email, string Password, string ConfirmPassword, string FirstName, string LastName)
        : IRequest<Response<string>>;
}
