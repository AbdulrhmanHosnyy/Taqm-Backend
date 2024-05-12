using Taqm.Data.Entities;
using Taqm.Data.Responses;

namespace Taqm.Core.Mappings.Posts
{
    public partial class PostProfile
    {
        public void GetPostPaginatedListMapping()
        {
            CreateMap<Post, GetPostPagintaedListResponse>()
                .ForMember(dest => dest.SellerLocation, opt => opt.MapFrom(src => $"{src.User.City} {src.User.Region}"));
        }
    }
}
