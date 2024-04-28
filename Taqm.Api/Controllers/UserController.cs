using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Taqm.Api.Bases;
using Taqm.Core.Features.Users.Commands.Models;
using Taqm.Core.Features.Users.Queries.Models;
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

        [Authorize]
        [HttpGet(Router.UserRouting.GetAll)]
        public async Task<IActionResult> GetAll() =>
            NewResult(await Mediator.Send(new GetUsersListQuery()));

        [HttpGet(Router.UserRouting.GetById)]
        public async Task<IActionResult> GetById(int id) =>
            NewResult(await Mediator.Send(new GetUserByIdQuery(id)));

        [HttpGet(Router.UserRouting.GetByIdIncludingPosts)]
        public async Task<IActionResult> GetByIdIncludingPosts(int id) =>
            NewResult(await Mediator.Send(new GetUserByIdIncludingPostsQuery(id)));

        [HttpPut(Router.UserRouting.Update)]
        public async Task<IActionResult> Update([FromForm] UpdateUserCommand updateUserCommand) =>
            NewResult(await Mediator.Send(updateUserCommand));

        [HttpDelete(Router.UserRouting.Delete)]
        public async Task<IActionResult> Delete(int id) =>
            NewResult(await Mediator.Send(new DeleteUserCommand(id)));

        [HttpPost(Router.UserRouting.ChangePassword)]
        public async Task<IActionResult> ChangePassword(ChangeUserPasswordCommand changeUserPasswordCommand) =>
            NewResult(await Mediator.Send(changeUserPasswordCommand));

        [HttpPost(Router.UserRouting.ForgetPassword)]
        public async Task<IActionResult> ForgetPassword([Required] string email) =>
            NewResult(await Mediator.Send(new ForgetPasswordCommand(email)));
        #endregion
    }
}
