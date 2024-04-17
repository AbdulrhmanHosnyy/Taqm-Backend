using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IFileService _fileService;
        #endregion

        #region Constructors
        public UserService(UserManager<User> userManager, AppDbContext appDbContext, IHttpContextAccessor contextAccessor,
            IUrlHelper urlHelper, IEmailService emailService, IFileService fileService)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
            _contextAccessor = contextAccessor;
            _urlHelper = urlHelper;
            _emailService = emailService;
            _fileService = fileService;
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
        public async Task<User> GetUserByIdAsync(int id) => await _userManager.FindByIdAsync(id.ToString());
        public async Task<User> GetUserByIdIncludingPostsAsync(int id)
        {
            return await _userManager.Users
            .AsNoTracking()
            .AsQueryable()
            .Where(u => u.Id == id)
            .Include(u => u.Posts)
            .FirstOrDefaultAsync();
        }
        public async Task<string> UpdateAsync(User user, IFormFile file)
        {
            var context = _contextAccessor.HttpContext.Request;
            var baseUrl = context.Scheme + "://" + context.Host;
            var imageUrl = await _fileService.UploadImageAsync("Users", file);
            if (imageUrl == "FailedToUploadImage") return "FailedToUploadImage";
            else if (imageUrl == "NoImage") user.Image = null;
            else user.Image = baseUrl + imageUrl;
            try
            {
                await _userManager.UpdateAsync(user);
                return "Success";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }
        public async Task<string> DeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user is null) return "NotFound";

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded ? "Success" : "Failed";
        }
        public async Task<string> ChangePasswordAsync(int id, string currentPassword, string newPassword)
        {
            //  Check user existance
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user is null) return "Failed";

            //  Chnage Password
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            return result.Succeeded ? "Success" : result.Errors.FirstOrDefault().Description;

        }
        public async Task<string> ForgetPasswordAsync(string email)
        {
            var transaction = await _appDbContext.Database.BeginTransactionAsync();

            try
            {
                // Check user existance
                var user = await _userManager.FindByEmailAsync(email);
                if (user is null) return "NotFound";

                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                //  Reset Password link
                var requestAccessor = _contextAccessor.HttpContext.Request;
                var returnUrl = requestAccessor.Scheme + "://" + requestAccessor.Host +
                    _urlHelper.Action("ResetPasswordToken", "Authentication", new { Token = resetToken });
                var message = $@"
                    <html>
                    <head>
                        <title>Reset Password Token</title>
                    </head>
                    <body>
                        <p>You can the reset password token through this link:</p>
                        <a href='{returnUrl}'>{returnUrl}</a>
                    </body>
                    </html>";

                //  Send Confirm Url
                await _emailService.SendEmail(user.Email, "Reset Password Token", message);
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
