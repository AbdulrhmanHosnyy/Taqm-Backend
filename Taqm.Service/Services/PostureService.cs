using System.Linq.Expressions;
using Taqm.Data.Entities;
using Taqm.Infrastructure.Abstracts;
using Taqm.Service.Abstracts;

namespace Taqm.Service.Services
{
    public class PostureService : IPostureService
    {
        #region Fields
        private readonly IPostRepository _postRepository;
        #endregion

        #region Constructors
        public PostureService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        #endregion

        #region Methods
        public async Task<Post> GetByIdAsync(int id) => await _postRepository.GetByIdAsync(id);

        public async Task<List<Post>> GetListAsync(Expression<Func<Post, bool>> criteria, Expression<Func<Post, object>> orderBy = null,
            string orderByDirection = "ASC") => await _postRepository.FindAllAsync(criteria, orderBy, orderByDirection);

        public async Task<string> CreateAsync(Post posture)
        {
            await _postRepository.CreateAsync(posture);
            return "Success";
        }
        public async Task<string> UpdateAsync(Post posture)
        {
            await _postRepository.UpdateAsync(posture);
            return "Success";
        }
        public async Task<string> RenewAsync(int id)
        {
            Post post = await _postRepository.GetByIdAsync(id);
            post.CreatedAt = DateTime.Now;
            await _postRepository.UpdateAsync(post);
            return "Success";
        }
        public async Task<string> DeleteAsync(Post posture)
        {
            await _postRepository.DeleteAsync(posture);
            return "Success";
        }
        #endregion

    }
}
