using Taqm.Core.Features.Users.Commands.Models;
using Taqm.Data.Entities.Identity;

namespace Taqm.Core.Mappings.Users
{
    public partial class UserProfile
    {
        public void AddUserCommandMapping()
        {
            CreateMap<CreateUserCommand, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}
