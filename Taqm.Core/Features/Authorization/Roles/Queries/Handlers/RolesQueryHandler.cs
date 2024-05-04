using AutoMapper;
using Microsoft.Extensions.Localization;
using Taqm.Core.Bases;
using Taqm.Core.Resources;
using Taqm.Service.Abstracts;

namespace Taqm.Core.Features.Authorization.Claims.Queries.Handlers
{
    public class ClaimsQueryHandler : ResponseHandler
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

        #endregion
    }
}
