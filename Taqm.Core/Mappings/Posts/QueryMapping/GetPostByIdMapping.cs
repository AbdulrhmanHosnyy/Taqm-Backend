using Taqm.Core.Features.Posts.Queries.Responses;
using Taqm.Data.Entities;

namespace Taqm.Core.Mappings.Posts
{
    public partial class PostProfile
    {
        public void GetPostByIdMapping()
        {
            CreateMap<Post, GetPostByIdResponse>();
        }
    }
}
