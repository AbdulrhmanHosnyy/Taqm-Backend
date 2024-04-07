using MediatR;
using Taqm.Core.Bases;
using Taqm.Core.Features.Posts.Queries.Responses;

namespace Taqm.Core.Features.Posts.Queries.Models
{
    public record GetPostByIdQuery(int Id) : IRequest<Response<GetPostByIdResponse>>;
}
