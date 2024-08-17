using BiteBlogs.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace BiteBlogs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepositoryObj;
        private readonly ILogger<ImagesController> logger;

        public ImagesController(IImageRepository imageRepositoryObj, ILogger<ImagesController> logger)
        {
            this.imageRepositoryObj = imageRepositoryObj;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is null or empty.");
            }

             var imageURL = await imageRepositoryObj.UploadAsync(file);

            if (imageURL == null)
            {
                logger.LogError("Image upload failed.");
                return Problem("Abhi Something went wrong", null, (int)HttpStatusCode.InternalServerError);
            }

            return new JsonResult(new { link = imageURL });
        }
    }
}
