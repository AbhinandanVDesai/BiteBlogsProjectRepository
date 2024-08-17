using BiteBlogs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BiteBlogs.Repositories;

namespace BiteBlogs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogRepository blogRepository;
        private readonly ITagRepository tagRepository;

        public HomeController(ILogger<HomeController> logger,IBlogRepository blogRepository,ITagRepository tagRepository )
        {
            _logger = logger;
            this.blogRepository = blogRepository;
            this.tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index(string searchQuery)
        {

            //getting all blogs
            var blogPosts= await blogRepository.GetAllAsync(searchQuery);


            //getting all tags
            var tags=await tagRepository.GetAllAsync();

            

            //creating view model which contain both of this blog and tag

            var blogsWithTag = new BlogsTagsHomeView
            {
                blogPosts = blogPosts,
                tags = tags
            };

            return View(blogsWithTag);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Priv()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
