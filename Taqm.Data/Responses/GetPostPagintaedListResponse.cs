namespace Taqm.Data.Responses
{
    public class GetPostPagintaedListResponse
    {
        public GetPostPagintaedListResponse(int postId, string productImage, string productDescription,
            decimal productPrice, DateTime createdAt, string? sellerLocation)
        {
            PostId = postId;
            ProductImage = productImage;
            ProductDescription = productDescription;
            ProductPrice = productPrice;
            CreatedAt = createdAt;
            SellerLocation = sellerLocation;
        }

        public int PostId { get; set; }
        public string ProductImage { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? SellerLocation { get; set; }
    }
}
