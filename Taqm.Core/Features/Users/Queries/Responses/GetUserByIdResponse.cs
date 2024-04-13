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
        public List<GetUserByIdPostsResponse>? Posts { get; set; }
    }

    public class GetUserByIdPostsResponse
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string ProductImage { get; set; }
        public string ProductDescription { get; set; }
        public string? ProductCategory { get; set; }
        public string? ProductPrice { get; set; }
        public string? ProductCondition { get; set; }
        public string? ProductGender { get; set; }
        public string? ProductSeason { get; set; }
        public string? ProductColor { get; set; }
        public string? ProductSize { get; set; }
        public float? ProductWidth { get; set; }
        public float? ProductHeight { get; set; }
    }
}
