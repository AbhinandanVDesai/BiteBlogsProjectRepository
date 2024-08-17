using BiteBlogs.Models.NewFolder;
using BiteBlogs.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using BiteBlogs.Repositories;

namespace BiteBlogs.Controllers
{
    public class AddYourTagController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AddYourTagController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(addTagRequest);
            }

            var tag = new Tag
            {
               Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            try
            {
                await tagRepository.AddAsync(tag);
                ViewBag.Message = "Tag added successfully!";
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                return View(addTagRequest);
            }

            return View();
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List(
            string? searchQuery,
            string? sortBy,
            string? sortDirection,
            int pageSize=3,
            int pageNumber=1
            )
        {

            var totalRecords = await tagRepository.CountAsync();

            var totalPages = Math.Ceiling((decimal)totalRecords / pageSize);

            if (pageNumber > totalPages)
            {
                pageNumber--;                   //if the pageNumber exeeds totalpage when click on next, this will keep it in the range
            }

            if (pageNumber < 1)
            {
                pageNumber++;                  //if pageNumber become zero
            }
            



            ViewBag.pageNumber=pageNumber;
            ViewBag.pageSize=pageSize;
            ViewBag.TotalPages = totalPages;
            ViewBag.searchQuery = searchQuery;
            ViewBag.sortBy = sortBy;
            ViewBag.sortDirection=sortDirection;

            



            var addedTagList = await tagRepository.GetAllAsync(searchQuery,sortBy,sortDirection,pageSize,pageNumber);
            return View("addedTagList", addedTagList);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await tagRepository.GetAsync(id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,
                };

                return View(editTagRequest);
            }

            return NotFound("Tag not found.");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            Tag tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,

            };
           
            var updatedTag = await tagRepository.UpdateAsync(tag);
              
            
             return RedirectToAction("Edit", new {id=editTagRequest.Id});    
            

            
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(EditTagRequest editTagRequest)
        {
            
                    await tagRepository.DeleteAsync(editTagRequest.Id);
           
                    return RedirectToAction("Edit", new { id = editTagRequest.Id });
                
           

            
        }
    }
}
