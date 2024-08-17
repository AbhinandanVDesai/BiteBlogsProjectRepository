
using BiteBlogs.Models;
using Microsoft.EntityFrameworkCore;

namespace BiteBlogs.Repositories
{
    public class BlogPostLikesRepository : IBlogPostLikeRepository
    {
        private readonly BiteBlogDbContext biteBlogDbContext;

        public BlogPostLikesRepository(BiteBlogDbContext biteBlogDbContext)
        {
            this.biteBlogDbContext = biteBlogDbContext;
        }

      
        public async Task<int> GetAllLikes(Guid BlogPostId)
        {

            return await biteBlogDbContext.BlogPostLikes.CountAsync(x => x.BlogpostId == BlogPostId);


        }





        public async Task<BlogPostLike> AddLikeAsync(BlogPostLike blogPostLikeData)
        {
            await biteBlogDbContext.BlogPostLikes.AddAsync(blogPostLikeData);
            await biteBlogDbContext.SaveChangesAsync();
            return blogPostLikeData;

        }

        public async Task<IEnumerable<BlogPostLike>> GetAllLikesForThePerticularBlog(Guid BlogPostId)
        {
           return  await biteBlogDbContext.BlogPostLikes.Where(x => x.BlogpostId == BlogPostId).ToListAsync();
        }

        public async Task<BlogPostLike?> RemoveLikeAsync(Guid userId, Guid blogPostId)
        {
            // Find the like by the user for the specific blog post
            var likedByTheSignedInUser = await biteBlogDbContext.BlogPostLikes
                .FirstOrDefaultAsync(x => x.UserId == userId && x.BlogpostId == blogPostId);

            if (likedByTheSignedInUser == null)
            {
                // If no like is found, return null or handle accordingly
                return null;
            }

            // Remove the like
            biteBlogDbContext.BlogPostLikes.Remove(likedByTheSignedInUser);

            // Save changes to the database
            await biteBlogDbContext.SaveChangesAsync();

            // Return the removed like
            return likedByTheSignedInUser;
        }

    }
}
