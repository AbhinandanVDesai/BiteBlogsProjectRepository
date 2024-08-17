using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

public class demoImageUploadController : Controller
{
    // Replace with your Cloudinary credentials
    private readonly Cloudinary _cloudinary;

    public demoImageUploadController()
    {
        Account cloudinaryAccount = new Account(
            "dhppx80nt",    // CloudName
            "239843443952944", // ApiKey
            "kDE7H1TAB2UIk-E4asgsNd9nRQg" // ApiSecret
        );

        _cloudinary = new Cloudinary(cloudinaryAccount);
    }

    [HttpGet]
    public IActionResult Upload()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            ViewBag.Message = "No file uploaded.";
            return View();
        } 

        try
        {
            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    PublicId = "unique_image_id", // Optional: Unique identifier for the image
                };

                ImageUploadResult uploadResult = await _cloudinary.UploadAsync(uploadParams);

                ViewBag.Message = "Upload successful!";
                ViewBag.ImageUrl = uploadResult.SecureUrl.ToString();
            }
        }
        catch (Exception ex)
        {
            ViewBag.Message = "Error uploading image: " + ex.Message;
        }

        return View();
    }
}
