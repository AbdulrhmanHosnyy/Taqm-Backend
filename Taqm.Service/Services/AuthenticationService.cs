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
        private readonly SignInManager<User> _signInManager;
        private readonly JwtSettings _jwtSettings;
        #endregion

        #region Constructors
        public AuthenticationService(UserManager<User> userManager, JwtSettings jwtSettings,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _signInManager = signInManager;
        }
        #endregion

        #region Methods
        public async Task<JwtAuthResponse> SignInAsyns(string email, string password)
        {
            //  Check user existance
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null) return new JwtAuthResponse
            {
                Email = email,
                IsAuthenticated = false,
                Message = "EmailNotExist",
            };

            //  Check if the email is confirmed
            if (!await _userManager.IsEmailConfirmedAsync(user)) return new JwtAuthResponse
            {
                Email = email,
                IsAuthenticated = false,
                Message = "ConfirmEmail",
                RefreshToken = null
            };

            //  SingIn process
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!signInResult.Succeeded) return new JwtAuthResponse
            {
                Email = email,
                IsAuthenticated = false,
                Message = "IncorrectPassword",
            };

            //  Response Object
            return await GetJWTTokenAsync(user);
        }
        private async Task<JwtAuthResponse> GetJWTTokenAsync(User user)
        {
            //  Generate Access Token
            var accessToken = await GenerateJWTToken(user);

            //  Return object
            var response = new JwtAuthResponse
            {
                Email = user.Email,
                IsAuthenticated = true,
                Roles = (List<string>)await _userManager.GetRolesAsync(user),
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
            var userClaims = await GetUserClaimsAsync(user);

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
        private async Task<List<Claim>> GetUserClaimsAsync(User user)
        {
            //  Adding Claims
            var claims = new List<Claim>
            {
                new Claim(nameof(Data.Helpers.UserClaims.Id), user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            //  Adding Roles
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles) claims.Add(new Claim(ClaimTypes.Role, role));

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
        public async Task<string> ResetPasswordAsync(string password, string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null) return "NotFound";

            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, token, password);
            return resetPasswordResult.Succeeded ? "Success" : resetPasswordResult.Errors.FirstOrDefault()!.Description;
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
