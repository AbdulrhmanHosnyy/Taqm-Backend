using Taqm.Core.Features.Posts.Commands.Models;
using Taqm.Data.Entities;

namespace Taqm.Core.Mappings.Posts
{
    public partial class PostProfile
    {
        public void UpdatePostCommandMapping()
        {
            CreateMap<UpdatePostCommand, Post>()
                .ForMember(dest => dest.ProductImage, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore());
        }
    }
}
