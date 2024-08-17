using Microsoft.AspNetCore.Mvc;
using BiteBlogs.Repositories;
using BiteBlogs.Models.NewFolder;
using BiteBlogs.Models;
using Microsoft.AspNetCore.Identity;

namespace BiteBlogs.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogRepository blogRepository;
        private readonly IBlogPostLikeRepository blogPostLikeRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ICommentRepository commentRepository;

        public BlogsController(IBlogRepository blogRepository,
            IBlogPostLikeRepository blogPostLikeRepository,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ICommentRepository commentRepository)
        {
            this.blogRepository = blogRepository;
            this.blogPostLikeRepository = blogPostLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.commentRepository = commentRepository;
        }


        [HttpGet]
            public async Task<IActionResult> getByUrlHandle(string urlHandle)
        {
             var blogPost= await blogRepository.GetByUrlHandle(urlHandle);
            var blogWithLikes=new BlogWithLikesForViewModel();
            var likeStatus = false;
            
            if (blogPost != null) {
             var   CountOfLikes = await blogPostLikeRepository.GetAllLikes(blogPost.Id);

                if(signInManager.IsSignedIn(User))
                {
                    //get the total likes for this block perticularly

                    var AllLikesForPerticularBlog = await blogPostLikeRepository.GetAllLikesForThePerticularBlog(blogPost.Id);

                    var UserId = userManager.GetUserId(User);

                    if(UserId != null)
                    {
                        //this is a single row of BlockPostLike with the user id of signed in user;
                        var LikeByThisPerticularSignInUser =AllLikesForPerticularBlog.FirstOrDefault(x => x.UserId == Guid.Parse(UserId));

                        likeStatus = LikeByThisPerticularSignInUser != null;
                    }
                };




                // also including comment property to the BlogWithLikesForViewModel

                var CommentsFromDomain = await commentRepository.GetAllCommentAsync(blogPost.Id);

                var allCommentsForViewModel = new List<BlogPostCommentViewModel>();
                
                 foreach(var blogComment in CommentsFromDomain)
                {
                    allCommentsForViewModel.Add(new BlogPostCommentViewModel
                    {
                        CommentDescription = blogComment.Comment,
                        CommentPostDate = blogComment.CommentDate,
                        UserName = (await userManager.FindByIdAsync(blogComment.UserId.ToString())).UserName
                    }) ;



                }

                 blogWithLikes = new BlogWithLikesForViewModel
                {
                    Id = blogPost.Id,
                    Author = blogPost.Author,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    ShortDescription = blogPost.ShortDescription,
                    Content = blogPost.Content,
                    FeaturedUrl = blogPost.FeaturedUrl,
                    UrlHandle = blogPost.UrlHandle,
                    Likes = CountOfLikes,                        //i have added like property in this model, this is a count of all previous liked in the database
                    PublishedDate = blogPost.PublishedDate,
                    
                     LikeStatus = likeStatus,
                    AllCommentsForThisBlog = allCommentsForViewModel

                 };


              }
            return View(blogWithLikes);
        }


        [HttpPost]
        public async Task<IActionResult> AddComment(BlogWithLikesForViewModel CommentedBlog)
        {                                                         //the BlogWithLikeForViewModel contains lot of data but we just want comment string
           
            if(signInManager.IsSignedIn(User))
            {
                var commentToAdd = new BlogPostComment
                {
                   
                    Comment = CommentedBlog.Comment,                   // this is from request
                    UserId = Guid.Parse(userManager.GetUserId(User)), //user id we are getting from user manager
                    BlogPostId = CommentedBlog.Id,                   //blog id we are getting from the post request
                    CommentDate = DateTime.Now                       //this is from predifined method

                };

               await  commentRepository.AddCommentAsync(commentToAdd);
                //return RedirectToAction();
                return RedirectToAction("getByUrlHandle", "Blogs", new {urlHandle=CommentedBlog.UrlHandle});
            }
            return RedirectToAction("getByUrlHandle", "Blogs", new {urlHandle=CommentedBlog.UrlHandle});
            
            }
  
    }
}
