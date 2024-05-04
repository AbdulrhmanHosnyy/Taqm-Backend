using AutoMapper;

namespace Taqm.Core.Mappings.Authorization.Roles
{
    public partial class RolesProfile : Profile
    {
        public RolesProfile()
        {
            GetRoleResponseMapping();
        }
    }
}
