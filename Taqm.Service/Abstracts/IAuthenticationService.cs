using Taqm.Data.Entities.Identity;
using Taqm.Data.Responses;

namespace Taqm.Service.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<string> ConfirmEmailAsync(int? userId, string? code);
        public Task<string> ResetPasswordAsync(string password, string email, string token);
        public Task<JwtAuthResponse> GetJWTTokenAsync(User user);
        public Task<JwtAuthResponse> GetRefreshTokenAsync(string token);
        public Task<bool> RevokeTokenAsync(string token);
    }
}
