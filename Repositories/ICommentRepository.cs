using BiteBlogs.Models;

namespace BiteBlogs.Repositories
{
    public interface ICommentRepository
    {
        Task<BlogPostComment> AddCommentAsync(BlogPostComment comment);

        Task<IEnumerable<BlogPostComment>> GetAllCommentAsync(Guid blogPostId);
    }
}
