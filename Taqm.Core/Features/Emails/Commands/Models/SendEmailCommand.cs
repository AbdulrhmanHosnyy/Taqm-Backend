using MediatR;
using Taqm.Core.Bases;

namespace Taqm.Core.Features.Emails.Commands.Models
{
    public record class SendEmailCommand(string Email, string Subject, string Message) : IRequest<Response<string>>;
}
