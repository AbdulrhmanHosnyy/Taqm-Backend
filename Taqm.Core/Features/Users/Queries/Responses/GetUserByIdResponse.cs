namespace Taqm.Core.Features.Users.Queries.Responses
{
    public class GetUserByIdResponse
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? Gender { get; set; }
        public string? Image { get; set; }
        public float? Weight { get; set; }
        public float? Height { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
