using Microsoft.EntityFrameworkCore;
using Taqm.Data.Entities;
using Taqm.Infrastructure.Abstracts;
using Taqm.Infrastructure.Data;

namespace Taqm.Infrastructure.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        #region Fields
        private readonly DbSet<Post> _posts;
        #endregion

        #region Constructors
        public PostRepository(AppDbContext context) : base(context)
        {
            _posts = context.Set<Post>();
        }
        #endregion

        #region Methods

        #endregion
    }
}
