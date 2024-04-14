using Microsoft.AspNetCore.Http;
using Taqm.Data.Entities.Identity;

namespace Taqm.Service.Abstracts
{
    public interface IUserService
    {
        public Task<string> CreateAsync(User user, string password);
        public Task<string> UpdateAsync(User user, IFormFile file);
    }
}
