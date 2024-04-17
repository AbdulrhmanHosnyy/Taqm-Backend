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

        #region Methods
        public async Task<string> ConfirmEmailAsync(int? userId, string? code)
        {
            if (userId is null || code is null) return "ErrorConfirmEmail";
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var confirmEmail = await _userManager.ConfirmEmailAsync(user, code);
            if (!confirmEmail.Succeeded) return "ErrorConfirmEmail";
            return "Success";
        }
        public async Task<string> ResetPasswordAsync(string password, string email, string token)
        {
            // Check user existance
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null) return "NotFound";

            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, token, password);
            if (resetPasswordResult.Succeeded) return "Success";
            return resetPasswordResult.Errors.FirstOrDefault().Description;
        }
        #endregion
    }
}
