namespace BiteBlogs.Models.NewFolder
{
    public class BlogPostLikeRequest
    {
        public Guid BlogPostId { get; set; }

        public  Guid UserId { get; set; }
    }
}
