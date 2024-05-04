using Taqm.Core.Features.Authorization.Roles.Queries.Responses;
using Taqm.Data.Entities.Identity;

namespace Taqm.Core.Mappings.Authorization.Roles
{
    public partial class RolesProfile
    {
        public void GetRoleResponseMapping()
        {
            CreateMap<Role, GetRoleResponse>();
        }
    }
}
