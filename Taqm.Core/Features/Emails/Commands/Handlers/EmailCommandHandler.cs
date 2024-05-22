using MediatR;
using Microsoft.Extensions.Localization;
using Taqm.Core.Bases;
using Taqm.Core.Features.Emails.Commands.Models;
using Taqm.Core.Resources;
using Taqm.Service.Abstracts;

namespace Taqm.Core.Features.Emails.Commands.Handlers
{
    public class EmailCommandHandler : ResponseHandler,
        IRequestHandler<SendEmailCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IEmailService _emailService;
        #endregion

        #region Constructors
        public EmailCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
           IEmailService emailsService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _emailService = emailsService;
        }
        #endregion

        #region Handlers
        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var response = await _emailService.SendEmailAsync(request.Email, request.Subject, request.Message);
            return response == "Success" ? Success<string>(_stringLocalizer[SharedResourcesKeys.SendEmailSuccess]) :
                                           BadRequest<string>(_stringLocalizer[SharedResourcesKeys.SendEmailFailed]);
        }
        #endregion
    }
}
