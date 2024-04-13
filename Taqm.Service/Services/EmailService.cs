using MailKit.Net.Smtp;
using MimeKit;
using Taqm.Data.Helpers;
using Taqm.Service.Abstracts;

namespace Taqm.Service.Services
{
    public class EmailService : IEmailService
    {
        #region Fields
        private readonly EmailSettings _emailSettings;
        #endregion

        #region Constructors
        public EmailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }
        #endregion

        #region Methods
        public async Task<string> SendEmail(string email, string Subject, string message)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, true);
                    client.Authenticate(_emailSettings.FromEmail, _emailSettings.Password);
                    var bodybuilder = new BodyBuilder
                    {
                        HtmlBody = $"{message}",
                        TextBody = "wellcome",
                    };
                    var msg = new MimeMessage
                    {
                        Body = bodybuilder.ToMessageBody()
                    };
                    msg.From.Add(new MailboxAddress(_emailSettings.MailBoxName, _emailSettings.MailBoxAddress));
                    msg.To.Add(new MailboxAddress("User", email));
                    //message.Subject = reason == null ? "No Submitted" : reason;
                    msg.Subject = Subject == null ? "No Submitted" : Subject;
                    await client.SendAsync(msg);
                    await client.DisconnectAsync(true);
                }
                return "Success";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }
        #endregion
    }
}
