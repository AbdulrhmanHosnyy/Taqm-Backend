using MediatR;
using Taqm.Core.Wrappers;
using Taqm.Data.Responses;

namespace Taqm.Core.Features.Chats.Queries.Models
{
    public record GetUserPaginatedChatRoomsQuery(int UserId, int PageNumber, int PageSize) : IRequest<PaginatedResult<GetUserChatRoomsResponse>>;
}
