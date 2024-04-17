using MediatR;
using Microsoft.Extensions.Localization;
using Taqm.Core.Bases;
using Taqm.Core.Features.Authentication.Queries.Models;
using Taqm.Core.Resources;
using Taqm.Service.Abstracts;

namespace Taqm.Core.Features.Authentication.Queries.Handlers
{
    public class AuthenticationQueryHandler : ResponseHandler,
        IRequestHandler<ConfirmEmailQuery, Response<string>>,
        IRequestHandler<ResetPasswordTokenQuery, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthenticationService _authenticationService;
        #endregion

        #region Constructors
        public AuthenticationQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
            IAuthenticationService authenticationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authenticationService = authenticationService;
        }
        #endregion

        #region Handlers
        public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            var confirmEmail = await _authenticationService.ConfirmEmailAsync(request.UserId, request.Code);
            if (confirmEmail == "ErrorConfirmEmail")
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.ErrorConfirmEmail]);
            return Success<string>(_stringLocalizer[SharedResourcesKeys.ConfirmEmailDone]);
        }
        public async Task<Response<string>> Handle(ResetPasswordTokenQuery request, CancellationToken cancellationToken)
        {
            return Success(request.token);
        }
        #endregion
    }
}
