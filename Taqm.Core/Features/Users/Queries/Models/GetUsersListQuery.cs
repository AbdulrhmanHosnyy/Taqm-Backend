using MediatR;
using Taqm.Core.Bases;
using Taqm.Core.Features.Users.Queries.Responses;

namespace Taqm.Core.Features.Users.Queries.Models
{
    public record GetUsersListQuery : IRequest<Response<List<GetUserByIdIncludingPostsResponse>>>;
}
