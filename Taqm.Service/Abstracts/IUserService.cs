using Microsoft.AspNetCore.Http;
using Taqm.Data.Entities.Identity;

namespace Taqm.Service.Abstracts
{
    public interface IUserService
    {
        public Task<string> CreateAsync(User user, string password);
        public Task<string> ConfirmCreateUserEmailAsync(int userId, string emailConfirmationToken);
        public Task<List<User>> GetAllIncludingPostsAsync();
        public Task<User> GetUserByIdAsync(int id);
        public Task<User> GetUserByIdIncludingPostsAsync(int id);
        public Task<string> UpdateAsync(User user, IFormFile file);
        public Task<string> DeleteAsync(int id);
        public Task<string> ChangePasswordAsync(int id, string currentPassword, string newPassword);
        public Task<string> ForgetPasswordAsync(string email);
    }
}
