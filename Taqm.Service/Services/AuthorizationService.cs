using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Taqm.Data.Entities.Identity;
using Taqm.Data.Helpers;
using Taqm.Data.Requests;
using Taqm.Data.Responses;
using Taqm.Infrastructure.Data;
using Taqm.Service.Abstracts;

namespace Taqm.Service.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _appDbContext;
        #endregion

        #region Constructors
        public AuthorizationService(RoleManager<Role> roleManager, UserManager<User> userManager, AppDbContext appDbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _appDbContext = appDbContext;
        }
        #endregion

        #region Roles Methods
        public async Task<bool> CreateRoleAsync(string Name)
        {
            var identityRole = new Role();
            identityRole.Name = Name;
            var result = await _roleManager.CreateAsync(identityRole);
            return result.Succeeded;
        }
        public async Task<bool> IsRoleExistByNameAsync(string Name) => await _roleManager.RoleExistsAsync(Name);
        public async Task<string> UpdateRoleAsync(UpdateRoleRequest updateRoleRequest)
        {
            var role = await _roleManager.FindByIdAsync(updateRoleRequest.Id.ToString());
            if (role is null) return "NotFound";
            role.Name = updateRoleRequest.Name;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded) return "Success";
            var errors = string.Join("-", result.Errors);
            return errors;
        }
        public async Task<string> DeleteRoleAsync(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role is null) return "NotFound";

            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            if (users is not null && users.Count() > 0) return "Used";

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded) return "Success";

            var errors = string.Join("-", result.Errors);
            return errors;
        }
        public async Task<List<Role>> GetAllRolesAsync() => await _roleManager.Roles.ToListAsync();
        public async Task<Role> GetRoleByIdAsync(int id) => await _roleManager.FindByIdAsync(id.ToString());
        public async Task<ManageUserRolesResponse> ManageUserRolesAsync(User user)
        {
            //  Get all the roles
            var roles = await _roleManager.Roles.ToListAsync();

            var response = new ManageUserRolesResponse();
            response.UserId = user.Id;

            //  Check what roles the user has
            var rolesList = new List<UserRoles>();
            foreach (var role in roles)
            {
                var userRole = new UserRoles();
                userRole.Id = role.Id;
                userRole.Name = role.Name;
                if (await _userManager.IsInRoleAsync(user, role.Name))
                    userRole.HasRole = true;
                rolesList.Add(userRole);
            }

            //  Add the roles list to the response
            response.Roles = rolesList;

            return response;
        }
        public async Task<string> UpdateUserRolesAsync(UpdateUserRolesRequest updateUserRolesRequest)
        {
            var transaction = await _appDbContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(updateUserRolesRequest.Id.ToString());
                if (user is null) return "NotFound";

                var userRoles = await _userManager.GetRolesAsync(user);

                var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
                if (!removeResult.Succeeded) return "FailedToRemoveRoles";

                var addedResult = await _userManager.AddToRolesAsync(user, updateUserRolesRequest.RolesNames);
                if (!addedResult.Succeeded) return "FailedToAddRoles";

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

        #region Claims Methods
        public async Task<ManageUserClaimsResponse> ManageUserClaimsAsync(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);

            var response = new ManageUserClaimsResponse();
            response.UserId = user.Id;

            var claimsList = new List<Data.Responses.UserClaims>();
            foreach (var claim in ClaimsStore.claims)
            {
                var userClaim = new Data.Responses.UserClaims();
                userClaim.Type = claim.Type;
                if (userClaims.Any(x => x.Type == claim.Type))
                    userClaim.Value = true;
                claimsList.Add(userClaim);
            }

            response.Claims = claimsList;

            return response;
        }
        public async Task<string> UpdateUserClaimsAsync(UpdateUserClaimsRequest updateUserClaimsRequest)
        {
            var transaction = await _appDbContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(updateUserClaimsRequest.Id.ToString());
                if (user is null) return "NotFound";

                var userClaims = await _userManager.GetClaimsAsync(user);

                var removeResult = await _userManager.RemoveClaimsAsync(user, userClaims);
                if (!removeResult.Succeeded) return "FailedToRemoveClaims";

                var newClaims = updateUserClaimsRequest.ClaimsNames.Select(type => new Claim(type, "true"));
                var addedResult = await _userManager.AddClaimsAsync(user, newClaims);
                if (!addedResult.Succeeded) return "FailedToAddClaims";

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
