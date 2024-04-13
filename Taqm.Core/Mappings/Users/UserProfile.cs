using AutoMapper;

namespace Taqm.Core.Mappings.Users
{
    public partial class UserProfile : Profile
    {
        public UserProfile()
        {
            AddUserCommandMapping();
            GetUserByIdQueryMapping();
            UpdateUserCommandMapping();
        }
    }
}
