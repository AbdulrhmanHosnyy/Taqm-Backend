using Taqm.Data.Responses;

namespace Taqm.Service.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResponse> SignInAsyns(string email, string password);
        public Task<string> ResetPasswordAsync(string password, string email, string token);
        public Task<JwtAuthResponse> GetRefreshTokenAsync(string token);
        public Task<bool> RevokeTokenAsync(string token);
    }
}
