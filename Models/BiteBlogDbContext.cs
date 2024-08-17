using Microsoft.EntityFrameworkCore;

namespace BiteBlogs.Models
{
    public class BiteBlogDbContext : DbContext
    {
        public BiteBlogDbContext(DbContextOptions<BiteBlogDbContext> options) : base(options)
        {

        }

        public DbSet<BlogPost> BlogPosts{ get; set; }
        public DbSet<Tag>Tags{get;set;}

        public DbSet<BlogPostLike> BlogPostLikes { get; set; }
        public DbSet<BlogPostComment> BlogPostComments { get; set;}
    }
}
