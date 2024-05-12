using MediatR;
using Microsoft.AspNetCore.Http;
using Taqm.Core.Bases;

namespace Taqm.Core.Features.Posts.Commands.Models
{
    public record CreatePostCommand(int UserId, IFormFile ProductImage, string ProductCategory, string ProductDescription,
        decimal ProductPrice, string? ProductCondition, string? ProductGender, string? ProductColor, string? ProductSeason,
        string? ProductSize, float? ProductWidth, float? ProductHeight) : IRequest<Response<string>>;
}
