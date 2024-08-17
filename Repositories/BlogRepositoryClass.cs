using BiteBlogs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BiteBlogs.Repositories
{
    public class BlogRepositoryClass : IBlogRepository
    {
        private readonly BiteBlogDbContext dbContextObj;

        public BlogRepositoryClass(BiteBlogDbContext DbContextObj)
        {
            dbContextObj = DbContextObj;
        }


        //add a blog
        public async Task<BlogPost> AddBlogAsync(BlogPost Blog)
        {
           await dbContextObj. BlogPosts.AddAsync(Blog);
           await dbContextObj.SaveChangesAsync();
            return Blog;
        }
        
        public async Task<BlogPost?> DeleteBlogAsync(Guid id)
        {
            var existingBlog =  await dbContextObj.BlogPosts.FindAsync(id);

            if (existingBlog != null)
            {

                dbContextObj.BlogPosts.Remove(existingBlog);
                await dbContextObj.SaveChangesAsync();
                return existingBlog;

            }
            return null;
        }
        

        //get a list of all blogs which are present in a database
        public async Task<List<BlogPost>> GetAllAsync(
            string searchQuery,
            string sortBy,
            string sortDirection
            )
        {

            //filter

            var QueryableBlogs=dbContextObj.BlogPosts.Include(x=>x.Tags).AsQueryable();

               if(string.IsNullOrWhiteSpace(searchQuery) == false )
            {
                QueryableBlogs= QueryableBlogs.Where(x=>x.PageTitle.Contains(searchQuery)
                                             || x.Heading.Contains(searchQuery)  
                  );

            }


            //sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                var isDesc = string.Equals(sortDirection, "desc", StringComparison.OrdinalIgnoreCase);

                if (string.Equals(sortBy, "Heading", StringComparison.OrdinalIgnoreCase))
                {
                    QueryableBlogs = isDesc? QueryableBlogs.OrderByDescending(x => x.Heading) : QueryableBlogs.OrderBy(x => x.Heading);
                }

                if (string.Equals(sortBy, "Title", StringComparison.OrdinalIgnoreCase))
                {
                   QueryableBlogs= isDesc? QueryableBlogs.OrderByDescending(x=>x.PageTitle) : QueryableBlogs.OrderBy(x=>x.PageTitle);
                }
            }

   




               //pagination




               return await QueryableBlogs.ToListAsync();

           //return await dbContextObj.BlogPosts.Include(x => x.Tags).ToListAsync();
        }


        //get a single blog data , use for editing purpose
        public async Task<BlogPost?> GetAsync(Guid id)
        {
           return await dbContextObj.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x =>x.Id == id );

        }

        public async Task<BlogPost?> GetByUrlHandle(string urlHandle)
        {
            return await dbContextObj.BlogPosts.Include(x=>x.Tags)
                .FirstOrDefaultAsync(x => x.UrlHandle==urlHandle); 
        }

        public async Task<BlogPost?> UpdateBlogAsync(BlogPost blogToUpdate)
        {
            var existingBlog=await dbContextObj.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x => x.Id == blogToUpdate.Id);
            if (existingBlog != null)
            {
                existingBlog.Id = blogToUpdate.Id;
                existingBlog.Heading = blogToUpdate.Heading;
                existingBlog.ShortDescription = blogToUpdate.ShortDescription;
                existingBlog.Author = blogToUpdate.Author;
                existingBlog.Content = blogToUpdate.Content;
                existingBlog.FeaturedUrl= blogToUpdate.FeaturedUrl;
                existingBlog.PublishedDate = blogToUpdate.PublishedDate;
                existingBlog.Visible = blogToUpdate.Visible; 
                existingBlog.Tags= blogToUpdate.Tags;
                existingBlog.UrlHandle = blogToUpdate.UrlHandle;
                
                await dbContextObj.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }
    }
}
