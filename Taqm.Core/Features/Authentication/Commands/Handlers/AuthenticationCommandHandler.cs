using MediatR;
using Microsoft.Extensions.Localization;
using Taqm.Core.Bases;
using Taqm.Core.Features.Authentication.Commands.Models;
using Taqm.Core.Features.Users.Commands.Models;
using Taqm.Core.Resources;
using Taqm.Data.Responses;
using Taqm.Service.Abstracts;

namespace Taqm.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler : ResponseHandler,
        IRequestHandler<ResetPasswordCommand, Response<string>>,
        IRequestHandler<SignInCommand, Response<JwtAuthResponse>>,
        IRequestHandler<RevokeTokenCommand, Response<bool>>
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
        public async Task<Response<JwtAuthResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var response = await _authenticationService.SignInAsyns(request.Email, request.Password);
            switch (response.Message)
            {
                case "EmailNotExist":
                    return BadRequest<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.EmailIsNotExist]);
                case "ConfirmEmail":
                    return BadRequest<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.ConfirmEmail]);
                case "IncorrectPassword":
                    return BadRequest<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.IncorrectPassword]);
                case "Failed":
                    return BadRequest<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.IncorrectPassword]);
                default:
                    return !response.IsAuthenticated ? BadRequest<JwtAuthResponse>(response.Message!) : Success(response);
            }
        }
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
        public async Task<Response<bool>> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.RevokeTokenAsync(request.Token);
            if (result is false)
                return BadRequest<bool>(_stringLocalizer[SharedResourcesKeys.FailedToRevoke]);
            return Success(result);

        }
        public Task<Response<string>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
