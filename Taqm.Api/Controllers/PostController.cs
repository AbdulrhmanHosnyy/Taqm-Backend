using Microsoft.AspNetCore.Mvc;
using Taqm.Api.Bases;
using Taqm.Core.Features.Posts.Queries.Models;

namespace Taqm.Api.Controllers
{
    [ApiController]
    public class PostController : AppControllerBase
    {
        #region Actions
        [HttpGet("/Post/GetPostById/{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            return NewResult(await Mediator.Send(new GetPostByIdQuery(id)));
        }
        #endregion

    }
}
