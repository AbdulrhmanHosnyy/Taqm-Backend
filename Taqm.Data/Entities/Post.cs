using Taqm.Data.Entities.Identity;

namespace Taqm.Data.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string ProductImage { get; set; }
        public string ProductDescription { get; set; }
        public string? ProductCategory { get; set; }
        public decimal ProductPrice { get; set; }
        public string? ProductCondition { get; set; }
        public string? ProductGender { get; set; }
        public string? ProductSeason { get; set; }
        public string? ProductColor { get; set; }
        public string? ProductSize { get; set; }
        public float? ProductWidth { get; set; }
        public float? ProductHeight { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null;
    }
}
