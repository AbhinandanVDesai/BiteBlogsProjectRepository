using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BiteBlogs.Models.NewFolder;
using Microsoft.Identity.Client;
using BiteBlogs.Models;
using BiteBlogs.Repositories;
using System.Threading;

namespace BiteBlogs.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AddLikeToBlogController : ControllerBase
    {
        private readonly IBlogPostLikeRepository blogPostLikeRepository;

        //calling a constructor 

        public AddLikeToBlogController(IBlogPostLikeRepository blogPostLikeRepository)
        {
            this.blogPostLikeRepository = blogPostLikeRepository;
        }




        [HttpGet]
        [Route("Test")]
        public IActionResult Test()
        {
            return Ok("API is working");
        }



        //this api controller action method is for adding a like for a perticular blog

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
                Id = blogLikeData.BlogPostId,
                UserId = blogLikeData.UserId,
            };

            await blogPostLikeRepository.AddLikeAsync(blogPostLike);

            return Ok();
      
        }



        //api controller method to get total number of like count for that perticular program

        [HttpGet]
        [Route("{blogPostId:Guid}/totalLikes")]

        public async Task<IActionResult> GetTotalLikesForBlog([FromRoute] Guid blogpostId)
        {
           var totalLikes= await blogPostLikeRepository.GetAllLikes(blogpostId);
           
            return Ok(totalLikes);
        
        }


    }
}
