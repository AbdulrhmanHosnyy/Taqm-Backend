using Microsoft.AspNetCore.Http;
using System.Data.Entity;
using System.Linq.Expressions;
using Taqm.Data.Consts;
using Taqm.Data.Entities;
using Taqm.Data.Enums;
using Taqm.Data.Requests;
using Taqm.Data.Responses;
using Taqm.Infrastructure.Abstracts;
using Taqm.Infrastructure.Data;
using Taqm.Service.Abstracts;

namespace Taqm.Service.Services
{
    public class PostService : IPostService
    {
        #region Fields
        private readonly IPostRepository _postRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IFileService _fileService;
        private readonly AppDbContext _appDbContext;
        #endregion

        #region Constructors
        public PostService(IPostRepository postRepository, IHttpContextAccessor contextAccessor, IFileService fileService, AppDbContext appDbContext)
        {
            _postRepository = postRepository;
            _contextAccessor = contextAccessor;
            _fileService = fileService;
            _appDbContext = appDbContext;
        }
        #endregion

        #region Methods
        public async Task<string> CreateAsync(Post post, IFormFile file)
        {
            var transaction = await _appDbContext.Database.BeginTransactionAsync();
            try
            {
                var context = _contextAccessor.HttpContext.Request;
                var baseUrl = context.Scheme + "://" + context.Host;
                var imageUrl = await _fileService.UploadImageAsync("Posts", file);
                if (imageUrl == "FailedToUploadImage") return "FailedToUploadImage";
                else if (imageUrl == "NoImage") post.ProductImage = null!;
                else post.ProductImage = baseUrl + imageUrl;

                await _postRepository.CreateAsync(post);
                await transaction.CommitAsync();
                return "Success";
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return "Failed";
            }
        }
        public IQueryable<Post> GetListAsQuerable(Expression<Func<Post, bool>> filter,
            Expression<Func<Post, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            var query = _postRepository.GetTableNoTracking().Include(p => p.User);
            var processedQuery = _postRepository.FindAllAsync(query, filter, orderBy, orderByDirection);
            return processedQuery;
        }
        public Expression<Func<Post, bool>> FilterPostExpression(GetPostPaginatedListRequest getPostPaginatedListRequest)
        {
            return post =>
            (getPostPaginatedListRequest.UserId == null || post.UserId == getPostPaginatedListRequest.UserId) &&
            (string.IsNullOrEmpty(getPostPaginatedListRequest.ProductCategory) ||
            post.ProductCategory == getPostPaginatedListRequest.ProductCategory) &&
            (string.IsNullOrEmpty(getPostPaginatedListRequest.ProductGender) ||
            post.ProductGender == getPostPaginatedListRequest.ProductGender) &&
            (string.IsNullOrEmpty(getPostPaginatedListRequest.ProductSeason) ||
            post.ProductSeason == getPostPaginatedListRequest.ProductSeason) &&
            (string.IsNullOrEmpty(getPostPaginatedListRequest.ProductColor) ||
            post.ProductColor == getPostPaginatedListRequest.ProductColor) &&
            (string.IsNullOrEmpty(getPostPaginatedListRequest.ProductCondition) ||
            post.ProductCondition == getPostPaginatedListRequest.ProductCondition) &&
            (string.IsNullOrEmpty(getPostPaginatedListRequest.ProductSize) ||
            post.ProductSize == getPostPaginatedListRequest.ProductSize) &&
            (getPostPaginatedListRequest.MinPrice == null || post.ProductPrice >= getPostPaginatedListRequest.MinPrice) &&
            (getPostPaginatedListRequest.MaxPrice == null || post.ProductPrice <= getPostPaginatedListRequest.MaxPrice) &&
            (getPostPaginatedListRequest.MinWidth == null || post.ProductWidth >= getPostPaginatedListRequest.MinWidth) &&
            (getPostPaginatedListRequest.MaxWidth == null || post.ProductWidth <= getPostPaginatedListRequest.MaxWidth) &&
            (getPostPaginatedListRequest.MinHeight == null || post.ProductHeight >= getPostPaginatedListRequest.MinHeight) &&
            (getPostPaginatedListRequest.MaxHeight == null || post.ProductHeight <= getPostPaginatedListRequest.MaxHeight);
        }
        public Expression<Func<Post, object>> OrderPostExpression(PostOrderingEnum postOrderingEnum)
        {
            switch (postOrderingEnum)
            {
                case PostOrderingEnum.MostRelevant:
                    return post => post.PostId;
                case PostOrderingEnum.NewlyListed:
                    return post => post.CreatedAt;
                case PostOrderingEnum.LowestPrice:
                    return post => post.ProductPrice;
                case PostOrderingEnum.HighestPrice:
                    return post => post.ProductPrice;
                default:
                    return post => post.PostId;
            }
        }
        public Expression<Func<Post, GetPostPagintaedListResponse>> MapPostExpression()
        {
            return e =>
            new GetPostPagintaedListResponse(e.PostId, e.ProductImage, e.ProductDescription, e.ProductPrice, e.CreatedAt,
            $"{e.User.City} {e.User.Region}");
        }
        public async Task<Post> GetByIdAsync(int id) => await _postRepository.GetByIdAsync(id);
        public async Task<Post> GetByIdIncludingUserAsync(int id)
        {
            string[] includes = { "User" }; // Include the "User" navigation property
            Expression<Func<Post, bool>> criteria = p => p.PostId == id; // Define criteria to find post by ID

            return await _postRepository.FindAsync(criteria, includes);
        }

        public async Task<string> UpdateAsync(Post post, IFormFile file)
        {
            var context = _contextAccessor.HttpContext.Request;
            var baseUrl = context.Scheme + "://" + context.Host;
            var imageUrl = await _fileService.UploadImageAsync("Posts", file);
            if (imageUrl == "FailedToUploadImage") return "FailedToUploadImage";
            else if (imageUrl == "NoImage") post.ProductImage = null!;
            else post.ProductImage = baseUrl + imageUrl;
            try
            {
                await _postRepository.UpdateAsync(post);
                return "Success";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }
        public async Task<bool> RenewAsync(int id)
        {
            Post post = await GetByIdAsync(id);
            post.CreatedAt = DateTime.Now;
            try
            {
                await _postRepository.UpdateAsync(post);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<string> DeleteAsync(int id)
        {
            var post = await GetByIdAsync(id);
            if (post is null) return "NotFound";

            var result = _postRepository.DeleteAsync(post);

            return result != null ? "Success" : "Failed";
        }
        #endregion
    }
}
