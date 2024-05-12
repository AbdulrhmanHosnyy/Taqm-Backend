using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using Taqm.Data.Consts;
using Taqm.Data.Entities;
using Taqm.Data.Enums;
using Taqm.Data.Requests;
using Taqm.Data.Responses;

namespace Taqm.Service.Abstracts
{
    public interface IPostService
    {
        public Task<string> CreateAsync(Post post, IFormFile file);
        public IQueryable<Post> GetListAsQuerable(Expression<Func<Post, bool>> filter,
            Expression<Func<Post, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);
        public Expression<Func<Post, bool>> FilterPostExpression(GetPostPaginatedListRequest getPostPaginatedListRequest);
        public Expression<Func<Post, object>> OrderPostExpression(PostOrderingEnum postOrderingEnum);
        public Expression<Func<Post, GetPostPagintaedListResponse>> MapPostExpression();
        public Task<Post> GetByIdAsync(int id);
        public Task<Post> GetByIdIncludingUserAsync(int id);
        public Task<string> UpdateAsync(Post post, IFormFile file);
        public Task<bool> RenewAsync(int id);
        public Task<string> DeleteAsync(int id);
    }
}
