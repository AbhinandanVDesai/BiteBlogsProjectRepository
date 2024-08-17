namespace BiteBlogs.Models.NewFolder
{
    public class BlogWithLikesForViewModel
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedUrl { get; set; }

        public string UrlHandle { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Author { get; set; }
        public bool Visible { get; set; }

        public int Likes {  get; set; }   //this property show number of likes previously for that blog

        public bool LikeStatus  { get; set; }

        public string Comment { get; set; }           //this property for posting new comment from view model to controller 

        public List<BlogPostCommentViewModel> AllCommentsForThisBlog { get; set; }  //this property is for dispalaying blog details with previous comments on the blog



        //Navigation property
        public ICollection<Tag> Tags { get; set; }
    }
}
