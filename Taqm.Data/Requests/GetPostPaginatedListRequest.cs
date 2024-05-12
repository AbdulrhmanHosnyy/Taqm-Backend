namespace Taqm.Data.Requests
{
    public record GetPostPaginatedListRequest(int? UserId, string? ProductCategory, string? ProductGender, string? ProductSeason,
        string? ProductColor, string? ProductCondition, string? ProductSize, decimal? MinPrice, decimal? MaxPrice,
        float? MinWidth, float? MaxWidth, float? MinHeight, float? MaxHeight);
}
