using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Taqm.Core.Bases;
using Taqm.Core.Features.Users.Queries.Models;
using Taqm.Core.Features.Users.Queries.Responses;
using Taqm.Core.Resources;
using Taqm.Service.Abstracts;

namespace Taqm.Core.Features.Users.Queries.Handlers
{
    public class UserQueryHandler : ResponseHandler,
        IRequestHandler<ConfirmCreateUserEmailQuery, Response<string>>,
        IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>,
        IRequestHandler<GetUserByIdIncludingPostsQuery, Response<GetUserByIdIncludingPostsResponse>>,
        IRequestHandler<GetUsersListQuery, Response<List<GetUserByIdIncludingPostsResponse>>>
    {
        #region Fields
        public readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public readonly IMapper _mapper;
        private readonly IUserService _userService;
        #endregion

        #region Constructors
        public UserQueryHandler(IStringLocalizer<SharedResources> stringLocalizer, IMapper mapper,
             IUserService userService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userService = userService;
        }
        #endregion

        #region Handlers
        public async Task<Response<string>> Handle(ConfirmCreateUserEmailQuery request, CancellationToken cancellationToken)
        {
            var confirmEmail = await _userService.ConfirmCreateUserEmailAsync(request.UserId, request.EmailConfirmationToken);
            return confirmEmail == "ErrorConfirmEmail" ? BadRequest<string>(_stringLocalizer[SharedResourcesKeys.ErrorConfirmEmail])
                                                       : Success<string>(_stringLocalizer[SharedResourcesKeys.ConfirmEmailDone]);
        }
        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByIdAsync(request.Id);
            if (user is null) return NotFound<GetUserByIdResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);

            var result = _mapper.Map<GetUserByIdResponse>(user);

            return Success(result);
        }

        public async Task<Response<GetUserByIdIncludingPostsResponse>> Handle(GetUserByIdIncludingPostsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByIdIncludingPostsAsync(request.Id);
            if (user is null) return NotFound<GetUserByIdIncludingPostsResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);

            var result = _mapper.Map<GetUserByIdIncludingPostsResponse>(user);

            return Success(result);
        }

        public async Task<Response<List<GetUserByIdIncludingPostsResponse>>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var userList = await _userService.GetAllIncludingPostsAsync();
            var mappedUserList = _mapper.Map<List<GetUserByIdIncludingPostsResponse>>(userList);
            return Success(mappedUserList);
        }

        #endregion

    }
}
