using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Taqm.Core.Bases;
using Taqm.Core.Features.Authorization.Claims.Queries.Models;
using Taqm.Core.Resources;
using Taqm.Data.Responses;
using Taqm.Service.Abstracts;

namespace Taqm.Core.Features.Authorization.Roles.Queries.Handlers
{
    public class ClaimsQueryHandler : ResponseHandler,
        IRequestHandler<ManageUserClaimsQuery, Response<ManageUserClaimsResponse>>

    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public ClaimsQueryHandler(IStringLocalizer<SharedResources> stringLocalizer, IAuthorizationService authorizationService, IMapper mapper, IUserService userService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            _mapper = mapper;
            _userService = userService;
        }
        #endregion

        #region Handlers
        public async Task<Response<ManageUserClaimsResponse>> Handle(ManageUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByIdAsync(request.UserId);
            if (user is null) return NotFound<ManageUserClaimsResponse>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);

            return Success(await _authorizationService.ManageUserClaimsAsync(user));
        }
        #endregion
    }
}
