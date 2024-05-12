using Microsoft.AspNetCore.Mvc;
using Taqm.Api.Bases;
using Taqm.Core.Features.Posts.Commands.Models;
using Taqm.Core.Features.Posts.Queries.Models;
using Taqm.Data.MetaData;

namespace Taqm.Api.Controllers
{
    [ApiController]
    public class PostController : AppControllerBase
    {
        #region Operations
        [HttpPost(Router.PostRouting.Create)]
        public async Task<IActionResult> Create([FromForm] CreatePostCommand createPostCommand) =>
            NewResult(await Mediator.Send(createPostCommand));
        [HttpGet(Router.PostRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id) =>
            Ok(await Mediator.Send(new GetPostByIdQuery(id)));
        [HttpGet(Router.PostRouting.GetPaginatedList)]
        public async Task<IActionResult> GetPaginatedList([FromQuery] GetPostPaginatedListQuery getPostPaginatedListQuery) =>
            Ok(await Mediator.Send(getPostPaginatedListQuery));
        [HttpPut(Router.PostRouting.Update)]
        public async Task<IActionResult> Update([FromForm] UpdatePostCommand updatePostCommand) =>
            NewResult(await Mediator.Send(updatePostCommand));
        [HttpPut(Router.PostRouting.Renew)]
        public async Task<IActionResult> Renew([FromRoute] int id) =>
            NewResult(await Mediator.Send(new RenewPostCommand(id)));
        [HttpDelete(Router.PostRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id) =>
            NewResult(await Mediator.Send(new DeletePostCommand(id)));
        #endregion

    }
}
