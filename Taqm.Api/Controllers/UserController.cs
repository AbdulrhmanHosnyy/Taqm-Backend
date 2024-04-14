using Microsoft.AspNetCore.Mvc;
using Taqm.Api.Bases;
using Taqm.Core.Features.Users.Commands.Models;
using Taqm.Data.MetaData;

namespace Taqm.Api.Controllers
{
    [ApiController]
    public class UserController : AppControllerBase
    {
        #region Operations
        [HttpPost(Router.UserRouting.Create)]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand createUserCommand) =>
            NewResult(await Mediator.Send(createUserCommand));

        [HttpPut(Router.UserRouting.Update)]
        public async Task<IActionResult> Update([FromForm] UpdateUserCommand updateUserCommand) =>
            NewResult(await Mediator.Send(updateUserCommand));
        #endregion
    }
}
