using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Taqm.Core.Bases;
using Taqm.Core.Features.Users.Commands.Models;
using Taqm.Core.Resources;
using Taqm.Data.Entities.Identity;
using Taqm.Service.Abstracts;

namespace Taqm.Core.Features.Users.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
        IRequestHandler<CreateUserCommand, Response<string>>,
        IRequestHandler<UpdateUserCommand, Response<string>>,
        IRequestHandler<DeleteUserCommand, Response<string>>,
        IRequestHandler<ChangeUserPasswordCommand, Response<string>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserService _userService;
        #endregion

        #region Constructors
        public UserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IMapper mapper,
            UserManager<User> userManager, IHttpContextAccessor contextAccessor, IUserService userService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _userService = userService;
        }
        #endregion

        #region Handlers
        public async Task<Response<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //  Mapping User
            var user = _mapper.Map<User>(request);

            // Create User
            var result = await _userService.CreateAsync(user, request.Password);
            switch (result)
            {
                case "EmailIsExist":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailIsExist]);
                case "ConfirmEmail":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.ConfirmEmail]);
                case "ErrorInCreateUser":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FaildToAddUser]);
                case "Failed":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.TryToRegisterAgain]);
                case "Success":
                    return Success<string>("");
                default:
                    return BadRequest<string>(result);
            }
        }

        public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            //  Check user existance
            var oldUser = await _userManager.FindByIdAsync(request.Id.ToString());
            if (oldUser is null) return NotFound<string>();

            //  Mapping
            var newUser = _mapper.Map(request, oldUser);

            //  Updating
            var result = await _userService.UpdateAsync(newUser, request.Image);

            // Return message
            switch (result)
            {
                case "FailedToUploadImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUploadImage]);
                case "Failed": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UpdateFailed]);
            }
            return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            //  Check user existance
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user is null) return NotFound<string>();

            //  Deleting
            var result = await _userManager.DeleteAsync(user);

            //  Return message
            if (!result.Succeeded) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.DeleteFailed]);
            return Success((string)_stringLocalizer[SharedResourcesKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            //  Check user existance
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user is null) return NotFound<string>();

            //  Chnage Password
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            //  Return message
            if (!result.Succeeded) return BadRequest<string>(result.Errors.FirstOrDefault().Description);
            return Success((string)_stringLocalizer[SharedResourcesKeys.PasswordChanged]);
        }
        #endregion

    }
}
