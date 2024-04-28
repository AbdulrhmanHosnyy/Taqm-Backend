namespace Taqm.Service.Abstracts
{
    public interface IEmailService
    {

        public Task<string> SendEmail(string email, string subject, string message);

        public Task<string> SendEmailAsync(string email, string subject, string message);

    }
}
