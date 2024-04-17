namespace Taqm.Service.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<string> ConfirmEmailAsync(int? userId, string? code);
        public Task<string> ResetPasswordAsync(string password, string email, string token);
    }
}
