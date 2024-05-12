using MediatR;
using Microsoft.AspNetCore.Http;
using Taqm.Core.Bases;

namespace Taqm.Core.Features.Posts.Commands.Models
{
    public record UpdatePostCommand(int PostId, IFormFile ProductImage, string ProductCategory, decimal ProductPrice,
        string? ProductCondition, string? ProductGender, string? ProductColor, string? ProductSeason,
        string? ProductSize, float? ProductWidth, float? ProductHeight) : IRequest<Response<string>>;
}
