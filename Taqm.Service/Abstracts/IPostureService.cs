using System.Linq.Expressions;
using Taqm.Data.Consts;
using Taqm.Data.Entities;

namespace Taqm.Service.Abstracts
{
    public interface IPostureService
    {
        public Task<Post> GetByIdAsync(int id);
        public Task<List<Post>> GetListAsync(Expression<Func<Post, bool>> criteria,
            Expression<Func<Post, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);
        public Task<string> CreateAsync(Post posture);
        public Task<string> UpdateAsync(Post posture);
        public Task<string> RenewAsync(int id);
        public Task<string> DeleteAsync(Post posture);
    }
}
