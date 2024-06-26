﻿using MediatR;
using Microsoft.Extensions.Localization;
using Taqm.Core.Bases;
using Taqm.Core.Features.Authentication.Queries.Models;
using Taqm.Core.Resources;
using Taqm.Data.Responses;
using Taqm.Service.Abstracts;

namespace Taqm.Core.Features.Authentication.Queries.Handlers
{
    public class AuthenticationQueryHandler : ResponseHandler,
        IRequestHandler<ResetPasswordTokenQuery, Response<string>>,
        IRequestHandler<CheckRefreshTokenQuery, Response<JwtAuthResponse>>
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
        public async Task<Response<string>> Handle(ResetPasswordTokenQuery request, CancellationToken cancellationToken)
        {
            return Success(request.token);
        }
        public async Task<Response<JwtAuthResponse>> Handle(CheckRefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.GetRefreshTokenAsync(request.RefreshToken);

            if (!result.IsAuthenticated) return BadRequest<JwtAuthResponse>(result.Message);

            return Success(result);
        }
        #endregion
    }
}
