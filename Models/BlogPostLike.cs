using Microsoft.EntityFrameworkCore;

namespace BiteBlogs.Models
{
    public class BlogPostLike
    {
        
        public Guid Id { get; set; }
        public  Guid BlogpostId { get; set; }
        public Guid UserId { get; set; }
    }
}
