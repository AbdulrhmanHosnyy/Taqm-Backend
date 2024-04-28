using Microsoft.EntityFrameworkCore;
namespace Taqm.Data.Entities.Identity
{
    [Owned]
    public class UserRefreshToken
    {
        public string Token { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
        public DateTime? RevokedOn { get; set; }
        public bool IsActive => RevokedOn == null && IsExpired == false;
    }
}
