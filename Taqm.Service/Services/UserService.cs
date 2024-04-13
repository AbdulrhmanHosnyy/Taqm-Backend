using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Taqm.Data.Entities.Identity;
using Taqm.Infrastructure.Data;
using Taqm.Service.Abstracts;

namespace Taqm.Service.Services
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _appDbContext;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUrlHelper _urlHelper;
        private readonly IEmailService _emailService;

        #endregion

        #region Constructors
        public UserService(UserManager<User> userManager, AppDbContext appDbContext, IHttpContextAccessor contextAccessor,
            IUrlHelper urlHelper, IEmailService emailService)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
            _contextAccessor = contextAccessor;
            _urlHelper = urlHelper;
            _emailService = emailService;
        }
        #endregion

        #region Methods
        public async Task<string> CreateAsync(User user, string password)
        {
            var transaction = await _appDbContext.Database.BeginTransactionAsync();

            try
            {
                //  Cheack email existance
                var userByEmail = await _userManager.FindByEmailAsync(user.Email);
                if (userByEmail is not null && userByEmail.EmailConfirmed) return "EmailIsExist";

                //  If not Exist Create user
                if (userByEmail is not null && !userByEmail.EmailConfirmed) return "ConfirmEmail";

                //  Create User
                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded) return string.Join(",", result.Errors.Select(e => e.Description).ToList());

                //  Confirm Email
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var requestAccessor = _contextAccessor.HttpContext.Request;
                var returnUrl = requestAccessor.Scheme + "://" + requestAccessor.Host +
                    _urlHelper.Action("ConfirmEmail", "Authentication", new { UserId = user.Id, Code = code });
                var message = $@"
                    <html>
                    <head>
                        <title>Confirm Email</title>
                    </head>
                    <body>
                        <p>To confirm your email, please click the following link:</p>
                        <a href='{returnUrl}'>{returnUrl}</a>
                    </body>
                    </html>";

                //  Send Confirm Url
                await _emailService.SendEmail(user.Email, "Confirm Email", message);
                await transaction.CommitAsync();
                return "Success";
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return "Failed";
            }
        }
        #endregion
    }
}
