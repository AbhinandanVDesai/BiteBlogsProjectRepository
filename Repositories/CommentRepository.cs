using BiteBlogs.Models;
using Microsoft.EntityFrameworkCore;

namespace BiteBlogs.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BiteBlogDbContext biteBlogDbContext;

        public CommentRepository(BiteBlogDbContext biteBlogDbContext)
        {
            this.biteBlogDbContext = biteBlogDbContext;
        }

        public async Task<BlogPostComment> AddCommentAsync(BlogPostComment comment)
        {
             await biteBlogDbContext.BlogPostComments.AddAsync(comment);
             await biteBlogDbContext.SaveChangesAsync();
            return comment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetAllCommentAsync(Guid blogPostId)
        {
            
            var allCommentsForThisBlog =await biteBlogDbContext.BlogPostComments.Where(x=>x.BlogPostId== blogPostId).ToListAsync();

            return allCommentsForThisBlog;
       }
    }
}
