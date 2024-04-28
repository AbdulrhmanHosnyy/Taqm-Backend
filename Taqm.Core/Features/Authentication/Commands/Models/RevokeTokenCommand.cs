using MediatR;
using Taqm.Core.Bases;

namespace Taqm.Core.Features.Authentication.Commands.Models
{
    public record RevokeTokenCommand(string? Token) : IRequest<Response<bool>>;
}
