using Microsoft.AspNetCore.Mvc;
using Taqm.Api.Bases;
using Taqm.Core.Features.Emails.Commands.Models;
using Taqm.Data.MetaData;

namespace Taqm.Api.Controllers
{
    [ApiController]
    public class EmailController : AppControllerBase
    {
        #region Operations
        [HttpPost(Router.EmailRouting.SendEmail)]
        public async Task<IActionResult> SendEmail([FromForm] SendEmailCommand sendEmailCommand) =>
            NewResult(await Mediator.Send(sendEmailCommand));
        #endregion
    }
}
