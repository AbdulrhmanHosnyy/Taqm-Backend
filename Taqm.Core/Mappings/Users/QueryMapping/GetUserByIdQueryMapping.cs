using Taqm.Core.Features.Users.Queries.Responses;
using Taqm.Data.Entities;
using Taqm.Data.Entities.Identity;

namespace Taqm.Core.Mappings.Users
{
    public partial class UserProfile
    {
        public void GetUserByIdQueryMapping()
        {
            CreateMap<User, GetUserByIdResponse>();
            CreateMap<Post, GetUserByIdPostsResponse>();
        }
    }
}
