namespace Taqm.Core.Features.Posts.Queries.Responses
{
    public class GetPostByIdResponse
    {
        public int PostId { get; set; }
        public DateTime CreatedAt { get; set; }
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
