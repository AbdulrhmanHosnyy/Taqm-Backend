using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Taqm.Data.Entities.Identity;
using Taqm.Data.Helpers;
using Taqm.Data.Responses;
using Taqm.Service.Abstracts;

namespace Taqm.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;
        #endregion

        #region Constructors
        public AuthenticationService(UserManager<User> userManager, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
        }
        #endregion

        #region Methods
        public async Task<string> ConfirmEmailAsync(int? userId, string? code)
        {
            if (userId is null || code is null) return "ErrorConfirmEmail";
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var confirmEmail = await _userManager.ConfirmEmailAsync(user, code);
            if (!confirmEmail.Succeeded) return "ErrorConfirmEmail";
            return "Success";
        }
        public async Task<string> ResetPasswordAsync(string password, string email, string token)
        {
            // Check user existance
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null) return "NotFound";

            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, token, password);
            if (resetPasswordResult.Succeeded) return "Success";
            return resetPasswordResult.Errors.FirstOrDefault().Description;
        }
        public async Task<JwtAuthResponse> GetJWTTokenAsync(User user)
        {
            //  Generate Access Token
            var accessToken = await GenerateJWTToken(user);

            //  Return object
            var response = new JwtAuthResponse
            {
                Email = user.Email,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(accessToken)
            };

            if (user.UserRefreshTokens.Any(t => t.IsActive))
            {
                var activeRefreshToken = user.UserRefreshTokens.FirstOrDefault(x => x.IsActive == true);
                response.RefreshToken = activeRefreshToken.Token;
                response.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;
            }
            else
            {
                var refreshToken = GenerateRefreshToken();
                response.RefreshToken = refreshToken.Token;
                response.RefreshTokenExpiration = refreshToken.ExpiresOn;
                user.UserRefreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(user);
            }
            return response;
        }
        private async Task<JwtSecurityToken> GenerateJWTToken(User user)
        {
            //  Getting user claims
            var userClaims = await GetClaimsAsync(user);

            //  Securing the key
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            //  Creating the token
            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                userClaims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: signingCredentials);

            return token;
        }
        private async Task<List<Claim>> GetClaimsAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(nameof(UserClaims.Id), user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            return claims;
        }
        private UserRefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var randomNumberGenerator = new RNGCryptoServiceProvider();
            randomNumberGenerator.GetBytes(randomNumber);

            return new UserRefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.Now.AddMinutes(_jwtSettings.RefreshTokenExpireDate),
                CreatedOn = DateTime.UtcNow
            };
        }
        public async Task<JwtAuthResponse> GetRefreshTokenAsync(string token)
        {
            var response = new JwtAuthResponse();
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserRefreshTokens.Any(t => t.Token == token));

            if (user is null)
            {
                response.Message = "Invalid Token";
                return response;
            }

            var refreshToken = user.UserRefreshTokens.Single(t => t.Token == token);

            //  Revoke Old Token
            if (!refreshToken.IsActive)
            {
                response.Message = "Inactive Token";
                return response;
            }
            refreshToken.RevokedOn = DateTime.UtcNow;

            //  Create New RefreshToken
            var newRefreshToken = GenerateRefreshToken();
            user.UserRefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);

            //  Create New Token
            var jwtToken = await GenerateJWTToken(user);
            response.IsAuthenticated = true;
            response.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            response.Email = user.Email;
            var roles = await _userManager.GetRolesAsync(user);
            response.Roles = roles.ToList();
            response.RefreshToken = newRefreshToken.Token;
            response.RefreshTokenExpiration = newRefreshToken.ExpiresOn;

            return response;
        }

        public async Task<bool> RevokeTokenAsync(string token)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserRefreshTokens.Any(t => t.Token == token));

            if (user is null) return false;

            var refreshToken = user.UserRefreshTokens.Single(t => t.Token == token);

            //  Revoke Old Token
            if (!refreshToken.IsActive) return false;

            refreshToken.RevokedOn = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            return true;
        }
        #endregion
    }
}
