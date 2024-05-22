using MediatR;
using Taqm.Core.Wrappers;
using Taqm.Data.Responses;

namespace Taqm.Core.Features.Chats.Queries.Models
{
    public record GetChatRoomMessagesQuery(int ChatRoomId, int PageNumber, int PageSize) : IRequest<PaginatedResult<GetChatRoomMessagesResponse>>;

}
