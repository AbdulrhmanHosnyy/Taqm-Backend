using MediatR;
using Taqm.Core.Wrappers;
using Taqm.Data.Enums;
using Taqm.Data.Requests;
using Taqm.Data.Responses;

namespace Taqm.Core.Features.Posts.Queries.Models
{
    public record GetPostPaginatedListQuery(int? UserId, string? ProductCategory, string? ProductGender, string? ProductSeason,
        string? ProductColor, string? ProductCondition, string? ProductSize, decimal? MinPrice, decimal? MaxPrice,
        float? MinWidth, float? MaxWidth, float? MinHeight, float? MaxHeight, PostOrderingEnum OrderBy, int PageNumber, int PageSize)

        : GetPostPaginatedListRequest(UserId, ProductCategory, ProductGender, ProductSeason, ProductColor, ProductCondition,
            ProductSize, MinPrice, MaxPrice, MinWidth, MaxWidth, MinHeight, MaxHeight),

         IRequest<PaginatedResult<GetPostPagintaedListResponse>>;
}
