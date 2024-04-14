using Taqm.Core.Features.Users.Commands.Models;
using Taqm.Data.Entities.Identity;

namespace Taqm.Core.Mappings.Users
{
    public partial class UserProfile
    {
        public void UpdateUserCommandMapping()
        {
            CreateMap<UpdateUserCommand, User>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
        }
    }
}
