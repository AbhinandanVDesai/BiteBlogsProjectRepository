using BiteBlogs.Models;

namespace BiteBlogs.Repositories
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetAllLikes(Guid BlogPostId);

        Task<IEnumerable<BlogPostLike>> GetAllLikesForThePerticularBlog(Guid BlogPostId);

        Task<BlogPostLike>AddLikeAsync(BlogPostLike blogPostLikeData);

        Task<BlogPostLike?> RemoveLikeAsync(Guid userId ,Guid BlogPostId);
    }
}
