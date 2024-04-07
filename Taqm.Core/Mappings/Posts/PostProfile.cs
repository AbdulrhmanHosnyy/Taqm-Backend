using AutoMapper;

namespace Taqm.Core.Mappings.Posts
{
    public partial class PostProfile : Profile
    {
        public PostProfile()
        {
            GetPostByIdMapping();
        }
    }
}
