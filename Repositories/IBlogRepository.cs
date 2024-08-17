using BiteBlogs.Models;

namespace BiteBlogs.Repositories
{
    public interface IBlogRepository
    {
        Task<List<BlogPost>> GetAllAsync(
            string? sortBy=null,
            string? sortDirection=null,
            string? searchQuery=null
            
            );

        Task<BlogPost?> GetAsync(Guid id);

        Task<BlogPost?>GetByUrlHandle(string urlHandle);

        Task<BlogPost> AddBlogAsync(BlogPost Blog);

        Task<BlogPost?> UpdateBlogAsync(BlogPost blogForUpdate);

        Task<BlogPost?> DeleteBlogAsync(Guid id);
    }
}
