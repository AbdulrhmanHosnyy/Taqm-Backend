using Microsoft.AspNetCore.Identity;

namespace Taqm.Data.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? Gender { get; set; }
        public string? Image { get; set; }
        public int? Age { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public DateTime LastSeen { get; set; }
        public float? Weight { get; set; }
        public float? Height { get; set; }

    }
}
