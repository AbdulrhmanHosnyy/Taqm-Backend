using Taqm.Data.Entities.Identity;
using Taqm.Data.Requests;
using Taqm.Data.Responses;

namespace Taqm.Service.Abstracts
{
    public interface IAuthorizationService
    {
        #region Roles Methods
        Task<bool> CreateRoleAsync(string Name);
        Task<bool> IsRoleExistByNameAsync(string Name);
        Task<string> UpdateRoleAsync(UpdateRoleRequest updateRoleRequest);
        Task<string> DeleteRoleAsync(int id);
        public Task<List<Role>> GetAllRolesAsync();
        public Task<Role> GetRoleByIdAsync(int id);
        public Task<ManageUserRolesResponse> ManageUserRolesAsync(User user);
        public Task<string> UpdateUserRolesAsync(UpdateUserRolesRequest updateUserRolesRequest);
        #endregion

        #region Claims Methods
        public Task<ManageUserClaimsResponse> ManageUserClaimsAsync(User user);
        public Task<string> UpdateUserClaimsAsync(UpdateUserClaimsRequest updateUserClaimsRequest);
        #endregion
    }
}
