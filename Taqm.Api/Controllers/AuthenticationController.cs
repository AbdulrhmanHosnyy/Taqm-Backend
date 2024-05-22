using Microsoft.AspNetCore.Mvc;
using Taqm.Api.Bases;
using Taqm.Core.Features.Authentication.Commands.Models;
using Taqm.Core.Features.Authentication.Queries.Models;
using Taqm.Data.MetaData;

namespace Taqm.Api.Controllers
{
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        #region Operations
        [HttpPost(Router.AuthenticationRouting.SignIn)]
        public async Task<IActionResult> SignIn([FromForm] SignInCommand signInCommand)
        {
            var result = await Mediator.Send(signInCommand);

            if (result.Succeeded)
                if (!string.IsNullOrEmpty(result.Data.RefreshToken))
                    SetRefreshTokenInCookie(result.Data.RefreshToken, result.Data.RefreshTokenExpiration);

            return NewResult(result);
        }
        private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime(),
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
        [HttpGet(Router.AuthenticationRouting.ResetPasswordToken)]
        public async Task<IActionResult> ResetPasswordToken([FromQuery] ResetPasswordTokenQuery resetPasswordTokenQuery) =>
            NewResult(await Mediator.Send(resetPasswordTokenQuery));
        [HttpPost(Router.AuthenticationRouting.ResetPassword)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand resetPasswordCommand) =>
            NewResult(await Mediator.Send(resetPasswordCommand));
        [HttpGet(Router.AuthenticationRouting.CheckRefreshToken)]
        public async Task<IActionResult> CheckRefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var result = await Mediator.Send(new CheckRefreshTokenQuery(refreshToken));

            if (result.StatusCode.Equals(200)) SetRefreshTokenInCookie(result.Data.RefreshToken, result.Data.RefreshTokenExpiration);

            return NewResult(result);
        }
        [HttpPost(Router.AuthenticationRouting.RevokeToken)]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenCommand revokeTokenCommand)
        {
            var token = revokeTokenCommand.Token ?? Request.Cookies["refreshToken"];
            var result = await Mediator.Send(new RevokeTokenCommand(token));
            return NewResult(result);
        }
        #endregion
    }
}
