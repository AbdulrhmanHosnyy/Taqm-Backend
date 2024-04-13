using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Taqm.Core.Bases;
using Taqm.Core.Features.Users.Queries.Models;
using Taqm.Core.Features.Users.Queries.Responses;
using Taqm.Core.Resources;
using Taqm.Data.Entities.Identity;

namespace Taqm.Core.Features.Users.Queries.Handlers
{
    public class UserQueryHandler : ResponseHandler,
        IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>
    {
        #region Fields
        public readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public readonly IMapper _mapper;
        public readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public UserQueryHandler(IStringLocalizer<SharedResources> stringLocalizer, IMapper mapper,
            UserManager<User> userManager) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
        }
        #endregion

        #region Handlers
        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (user is null) return NotFound<GetUserByIdResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);

            var result = _mapper.Map<GetUserByIdResponse>(user);

            return Success(result);
        }
        #endregion

    }
}
