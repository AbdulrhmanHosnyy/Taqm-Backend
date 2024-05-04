using Microsoft.AspNetCore.Mvc;
using Taqm.Api.Bases;
using Taqm.Core.Features.Authorization.Claims.Commands.Models;
using Taqm.Core.Features.Authorization.Claims.Queries.Models;
using Taqm.Core.Features.Authorization.Roles.Commands.Models;
using Taqm.Core.Features.Authorization.Roles.Queries.Models;
using Taqm.Data.MetaData;

namespace Taqm.Api.Controllers
{
    [ApiController]
    public class AuthorizationController : AppControllerBase
    {
        #region Roles Operations
        [HttpPost(Router.AuthorizationRouting.Create)]
        public async Task<IActionResult> Create([FromForm] CreateRoleCommand createRoleCommand) =>
            NewResult(await Mediator.Send(createRoleCommand));

        [HttpPut(Router.AuthorizationRouting.Update)]
        public async Task<IActionResult> Update([FromForm] UpdateRoleCommand updateRoleCommand) =>
            NewResult(await Mediator.Send(updateRoleCommand));

        [HttpDelete(Router.AuthorizationRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id) =>
            NewResult(await Mediator.Send(new DeleteRoleCommand(id)));

        [HttpGet(Router.AuthorizationRouting.GetAllRoles)]
        public async Task<IActionResult> GetAllRoles() =>
            NewResult(await Mediator.Send(new GetAllRolesQuery()));

        [HttpGet(Router.AuthorizationRouting.GetRoleById)]
        public async Task<IActionResult> GetRoleById([FromRoute] int id) =>
            NewResult(await Mediator.Send(new GetRoleByIdQuery(id)));

        [HttpGet(Router.AuthorizationRouting.ManageUserRoles)]
        public async Task<IActionResult> ManageUserRoles([FromRoute] int id) =>
            NewResult(await Mediator.Send(new ManageUserRolesQuery(id)));

        [HttpPut(Router.AuthorizationRouting.UpdateUserRoles)]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesCommand updateUserRolesCommand) =>
            NewResult(await Mediator.Send(updateUserRolesCommand));
        #endregion

        #region Claims Operations
        [HttpGet(Router.AuthorizationRouting.ManageUserClaims)]
        public async Task<IActionResult> ManageUserClaims([FromRoute] int id) =>
            NewResult(await Mediator.Send(new ManageUserClaimsQuery(id)));
        [HttpPut(Router.AuthorizationRouting.UpdateUserClaims)]
        public async Task<IActionResult> UpdateUserClaims([FromBody] UpdateUserClaimsCommand updateUserClaimsCommand) =>
            NewResult(await Mediator.Send(updateUserClaimsCommand));
        #endregion
    }
}
