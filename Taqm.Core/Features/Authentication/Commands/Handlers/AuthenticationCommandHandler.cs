using MediatR;
using Microsoft.Extensions.Localization;
using Taqm.Core.Bases;
using Taqm.Core.Features.Authentication.Commands.Models;
using Taqm.Core.Resources;
using Taqm.Service.Abstracts;

namespace Taqm.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler : ResponseHandler,
        IRequestHandler<ResetPasswordCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthenticationService _authenticationService;
        #endregion

        #region Constructors
        public AuthenticationCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
            IAuthenticationService authenticationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authenticationService = authenticationService;
        }
        #endregion

        #region Handlers
        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var resetPaswordResult =
                await _authenticationService.ResetPasswordAsync(request.Password, request.Email, request.Token);

            switch (resetPaswordResult)
            {
                case "NotFound":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailIsExist]);
                case "Failed":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToResetPassword]);
                case "Success":
                    return Success<string>(_stringLocalizer[SharedResourcesKeys.PasswordChanged]);
                default:
                    return BadRequest<string>(resetPaswordResult);
            }
        }
        #endregion
    }
}
