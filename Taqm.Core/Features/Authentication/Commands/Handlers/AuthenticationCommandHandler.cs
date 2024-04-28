using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Taqm.Core.Bases;
using Taqm.Core.Features.Authentication.Commands.Models;
using Taqm.Core.Resources;
using Taqm.Data.Entities.Identity;
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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        #endregion

        #region Constructors
        public AuthenticationCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
            IAuthenticationService authenticationService,
            UserManager<User> userManager,
            SignInManager<User> signInManager) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authenticationService = authenticationService;
            _userManager = userManager;
            _signInManager = signInManager;
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
        public async Task<Response<JwtAuthResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //  Check user existance
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null) return BadRequest<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.EmailIsNotExist]);

            //  SingIn process
            var signInResult = _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!signInResult.IsCompletedSuccessfully) return BadRequest<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.IncorrectPassword]);

            //  Response Object
            var result = await _authenticationService.GetJWTTokenAsync(user);
            if (!result.IsAuthenticated) return BadRequest<JwtAuthResponse>(result.Message);

            return Success(result);
        }
        public async Task<Response<bool>> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.RevokeTokenAsync(request.Token);
            if (result is false)
                return BadRequest<bool>(_stringLocalizer[SharedResourcesKeys.FailedToRevoke]);
            return Success(result);

        }
        #endregion
    }
}
