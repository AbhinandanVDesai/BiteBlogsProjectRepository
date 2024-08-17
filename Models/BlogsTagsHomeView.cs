namespace BiteBlogs.Models
{
    public class BlogsTagsHomeView
    {
        public IEnumerable<BlogPost> blogPosts { get; set; }

        public IEnumerable<Tag> tags { get; set; }
    }
}
