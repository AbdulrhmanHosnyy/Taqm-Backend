using MediatR;
using Microsoft.AspNetCore.Http;
using Taqm.Core.Bases;

namespace Taqm.Core.Features.Users.Commands.Models
{
    public record UpdateUserCommand(int Id, string FirstName, string LastName, string? City, string? Region, string? Gender,
        IFormFile? Image, string? Weight, string? Height, string? PhoneNumber) : IRequest<Response<string>>;

}
