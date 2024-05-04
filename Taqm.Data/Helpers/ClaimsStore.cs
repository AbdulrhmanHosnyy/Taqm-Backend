using System.Security.Claims;

namespace Taqm.Data.Helpers
{
    public static class ClaimsStore
    {
        public static List<Claim> claims = new List<Claim>()
        {
            new Claim("Get Posts", "false"),
        };
    }
}
