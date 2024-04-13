using Microsoft.AspNetCore.Mvc;
using Taqm.Api.Bases;
using Taqm.Core.Features.Authentication.Queries.Models;
using Taqm.Data.MetaData;

namespace Taqm.Api.Controllers
{
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        [HttpGet(Router.AuthenticationRouting.ConfirmEmail)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery confirmEmailQuery) =>
            NewResult(await Mediator.Send(confirmEmailQuery));
    }
}
