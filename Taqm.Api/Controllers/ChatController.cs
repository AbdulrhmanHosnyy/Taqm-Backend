using Microsoft.AspNetCore.Mvc;
using Taqm.Api.Bases;
using Taqm.Core.Features.Chats.Commands.Models;
using Taqm.Core.Features.Chats.Queries.Models;
using Taqm.Data.MetaData;

namespace Taqm.Api.Controllers
{
    [ApiController]
    public class ChatController : AppControllerBase
    {
        #region Operations
        [HttpPost(Router.ChatRouting.SendMessage)]
        public async Task<IActionResult> Create([FromForm] SendMessageCommand sendMessageCommand) =>
            NewResult(await Mediator.Send(sendMessageCommand));
        [HttpPut(Router.ChatRouting.UpdateMessage)]
        public async Task<IActionResult> UpdateMessage([FromForm] UpdateMessageCommand updateMessageCommand) =>
            NewResult(await Mediator.Send(updateMessageCommand));
        [HttpDelete(Router.ChatRouting.DeleteMessage)]
        public async Task<IActionResult> DeleteMessage([FromRoute] int id) =>
            NewResult(await Mediator.Send(new DeleteMessageCommand(id)));
        [HttpGet(Router.ChatRouting.GetUserChatRoomsPaginated)]
        public async Task<IActionResult> GetUserChatRoomsPaginated([FromQuery] GetUserPaginatedChatRoomsQuery getUserPaginatedChatRoomsQuery) =>
            Ok(await Mediator.Send(getUserPaginatedChatRoomsQuery));
        [HttpGet(Router.ChatRouting.GetChatRoomMessagesPaginated)]
        public async Task<IActionResult> GetChatRoomMessagesPaginated([FromQuery] GetChatRoomMessagesQuery getChatRoomMessagesQuery) =>
            Ok(await Mediator.Send(getChatRoomMessagesQuery));
        #endregion
    }
}
