using BiteBlogs.Models;
using BiteBlogs.Models.NewFolder;
using BiteBlogs.Repositories;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;

namespace BiteBlogs.Controllers
{
    public class AddBlogController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogRepository blogRepository;

        private readonly Cloudinary _cloudinary; //this is for using controller image upload
        public AddBlogController(ITagRepository tagRepository, IBlogRepository blogRepository)
        {
            this.tagRepository = tagRepository;
            this.blogRepository = blogRepository;


            Account cloudinaryAccount = new Account(
               "dhppx80nt",    // CloudName
               "239843443952944", // ApiKey
               "kDE7H1TAB2UIk-E4asgsNd9nRQg" // ApiSecret
               );

            _cloudinary = new Cloudinary(cloudinaryAccount);
        }


        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await tagRepository.GetAllAsync();


            var model = new AddBlogRequest                //when we are getting the view for adding blog  we are already passing a tags data(list of tags ) present in the database for select dropdown through the same AddBlogRequest model
            {
                // in this model of AddBlogRequest i am only adding a value for Tags propery and pass the model to the view
                Tags = tags.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.Id.ToString() })
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddBlogRequest AddBlogRequestObj,IFormFile FeaturedImageUpload)    //this IFormFile  FeaturedImageUpload parameter use come here with the help of name attribut in the input tag
        {

            if (FeaturedImageUpload != null && FeaturedImageUpload.Length > 0)
            {
                try
                {
                    using (var stream = FeaturedImageUpload.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(FeaturedImageUpload.FileName, stream),
                            PublicId = "unique_image_id" // Optional: Unique identifier for the image
                        };

                        ImageUploadResult uploadResult = await _cloudinary.UploadAsync(uploadParams);
                        AddBlogRequestObj.FeaturedUrl = uploadResult.SecureUrl.ToString();  // in this step i have assigned a url to the featuredUrl property of AddBlogRequest
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Error uploading image: " + ex.Message;
                    return View(AddBlogRequestObj);
                }
            }

            //map addBlogRequestObj to original model that is BlogPost which will eventually deals with database

            var blog = new BlogPost
            {
                Heading = AddBlogRequestObj.Heading,
                PageTitle = AddBlogRequestObj.PageTitle,
                Content = AddBlogRequestObj.Content,
                ShortDescription = AddBlogRequestObj.ShortDescription,
                Author = AddBlogRequestObj.Author,
                UrlHandle = AddBlogRequestObj.UrlHandle,
                FeaturedUrl = AddBlogRequestObj.FeaturedUrl,
                PublishedDate = AddBlogRequestObj.PublishedDate,


            };


            //Map Tags from selected tag
            var selectedTags = new List<Tag>();

            foreach (var selectedTagId in AddBlogRequestObj.SelectedTags)
            {
                var selectedTagIdGuid = Guid.Parse(selectedTagId);
                var existingTag = await tagRepository.GetAsync(selectedTagIdGuid);

                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }

            //mapping tags back to  main model
            blog.Tags = selectedTags;
            await blogRepository.AddBlogAsync(blog);
            return RedirectToAction("Add");
        }

        public async Task<IActionResult> GetAllBlogs(
            string searchQuery,
            string sortBy,
            string sortDirection
            )
        {
            var ListOfBlogs=await blogRepository.GetAllAsync(searchQuery,sortBy,sortDirection);

            return View(ListOfBlogs);
        }


        [HttpGet]
        [Route("AddBlog/EditBlog/{id}")]
        public async Task<IActionResult> EditBlog(Guid id)
        {
            var blogForEdit = await blogRepository.GetAsync(id);
            if (blogForEdit != null)
            {
                var allTags = await tagRepository.GetAllAsync();
                var editBlog = new EditBlogRequest
                {
                    Id = blogForEdit.Id,
                    Heading = blogForEdit.Heading,
                    PageTitle = blogForEdit.PageTitle,
                    Author = blogForEdit.Author,
                    Content = blogForEdit.Content,
                    ShortDescription = blogForEdit.ShortDescription,
                    FeaturedUrl = blogForEdit.FeaturedUrl,
                    UrlHandle = blogForEdit.UrlHandle,
                    PublishedDate = blogForEdit.PublishedDate,
                    Tags = allTags.Select(x => new SelectListItem
                    {
                        Text = x.DisplayName,
                        Value = x.Id.ToString()
                    }),
                    SelectedTags = blogForEdit.Tags.Select(x => x.Id.ToString()).ToArray()
                };
                return View(editBlog);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditBlog(EditBlogRequest editedBlog)
        {




            // we are getting blog for edit when we click on update button, form is posted for update and come here 
            //this is of type editBlogRequest which we have created for edit view , data is come in the form of editBlogRest type
            // now we have to map it in Blogpost type(domain model) object, and then send that object to repositories updateBlog method

            var BlogFromMainModel = new BlogPost()
            {
                Id = editedBlog.Id,
                PageTitle = editedBlog.PageTitle,
                Heading=editedBlog.Heading,
                Author = editedBlog.Author,
                Content= editedBlog.Content,
                ShortDescription = editedBlog.ShortDescription,
                FeaturedUrl= editedBlog.FeaturedUrl,
                PublishedDate= editedBlog.PublishedDate,
                UrlHandle   = editedBlog.UrlHandle,
                Visible = editedBlog.Visible,



            };



            //map tags into the domain model
            var selectedTags = new List<Tag>();
              
            foreach (var selectedTag in editedBlog.SelectedTags)   
            {
                if(Guid.TryParse(selectedTag,out var tag))
                {
                    var foundTag = await tagRepository.GetAsync(tag);

                    if(foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                }
            }

            BlogFromMainModel.Tags= selectedTags;

            //send BlogFromMainModel to BlogRepository for update

            var updatedBlog= await blogRepository.UpdateBlogAsync(BlogFromMainModel);

            if(updatedBlog != null)
            {
                //show successful update message

                RedirectToAction("EditBlog");
            }

            //show error message

            return View();
        }




        ///this httppost delete method is showing problem right now , i think the form is not get posted correctly

        //[HttpPost]
        //[Route("AddBlog/DeleteBlog/{id} ")]
        //public async Task<IActionResult> DeleteBlog(EditBlogRequest BlogToEdit)
        //{
        //      var DeletedBlog = await blogRepository.DeleteBlogAsync(BlogToEdit.Id);

        //    if (DeletedBlog != null)
        //    {
        //        //success message
        //        return RedirectToAction("GetAllTags");
        //    }

        //    return RedirectToAction("EditBlog",new { id = BlogToEdit.Id });

        //}



        //for temprory purpose i am using this method to delete the blog later on i will work on post delete method

        [Route("AddBlog/DeleteBlog/{id}")]
        public async Task<IActionResult> DeleteBlog(Guid id)
        {
            var deletedBlog = await blogRepository.DeleteBlogAsync(id);

            if (deletedBlog != null)
            {
                //success message

                return RedirectToAction("GetAllBlog");
            }

            return RedirectToAction("EditBlog", new { id = id });
        }





        //[Route("AddBlog/DeleteBlog/{id}")]

        //public  Guid DeleteBlog(Guid id)
        //{

        //    return id ;

        //}



    }
}
    