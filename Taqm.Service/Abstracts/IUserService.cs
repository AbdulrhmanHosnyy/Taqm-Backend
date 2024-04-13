using Taqm.Data.Entities.Identity;

namespace Taqm.Service.Abstracts
{
    public interface IUserService
    {
        public Task<string> CreateAsync(User user, string password);
    }
}
