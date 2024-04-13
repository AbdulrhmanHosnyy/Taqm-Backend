using Microsoft.AspNetCore.Identity;
using Taqm.Data.Entities.Identity;
using Taqm.Service.Abstracts;

namespace Taqm.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public AuthenticationService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        #endregion

        public async Task<string> ConfirmEmailAsync(int? userId, string? code)
        {
            if (userId is null || code is null) return "ErrorConfirmEmail";
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var confirmEmail = await _userManager.ConfirmEmailAsync(user, code);
            if (!confirmEmail.Succeeded) return "ErrorConfirmEmail";
            return "Success";
        }

    }
}
