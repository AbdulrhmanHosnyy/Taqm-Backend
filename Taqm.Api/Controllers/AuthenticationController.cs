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
        [HttpGet(Router.AuthenticationRouting.ConfirmEmail)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery confirmEmailQuery) =>
            NewResult(await Mediator.Send(confirmEmailQuery));

        [HttpGet(Router.AuthenticationRouting.ResetPasswordToken)]
        public async Task<IActionResult> ResetPasswordToken([FromQuery] ResetPasswordTokenQuery resetPasswordTokenQuery) =>
            NewResult(await Mediator.Send(resetPasswordTokenQuery));

        [HttpPost(Router.AuthenticationRouting.ResetPassword)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand resetPasswordCommand) =>
            NewResult(await Mediator.Send(resetPasswordCommand));
        #endregion
    }
}
