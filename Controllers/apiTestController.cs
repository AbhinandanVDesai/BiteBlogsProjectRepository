using BiteBlogs.Models.NewFolder;
using BiteBlogs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BiteBlogs.Repositories;

namespace BiteBlogs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class apiTestController : ControllerBase
    {
        private readonly IBlogPostLikeRepository blogPostLikeRepository;

        public apiTestController(IBlogPostLikeRepository blogPostLikeRepository)
        {
            this.blogPostLikeRepository = blogPostLikeRepository;
        }




        [HttpGet]
        [Route("test1")]
        public IActionResult test1()
        {
            return Ok("api is working nicely");
        }

        [HttpPost]
        [Route("postName")]
        public IActionResult PostName([FromBody] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Name cannot be empty.");
            }

            return Ok($"Hello, {name}!");


        }


        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] BlogPostLikeRequest blogLikeData)
        {
            if (blogLikeData == null)
            {
                return BadRequest("Invalid data.");
            }

            var blogPostLike = new BlogPostLike
            {
                BlogpostId = blogLikeData.BlogPostId,
                UserId = blogLikeData.UserId,
            };

            await blogPostLikeRepository.AddLikeAsync(blogPostLike);

            return Ok();

        }

    }
}
