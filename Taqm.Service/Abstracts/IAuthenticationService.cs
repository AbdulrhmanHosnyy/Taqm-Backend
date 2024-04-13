namespace Taqm.Service.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<string> ConfirmEmailAsync(int? userId, string? code);

    }
}
