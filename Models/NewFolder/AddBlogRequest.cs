using Microsoft.AspNetCore.Mvc.Rendering;

namespace BiteBlogs.Models.NewFolder
{
    public class AddBlogRequest
    {
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string? FeaturedUrl { get; set; }

        public string UrlHandle { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Author { get; set; }
        public bool Visible { get; set; }

        //list of tags available
        public IEnumerable<SelectListItem> Tags { get; set; }

        //selected tag
        public string[] SelectedTags { get; set; } = Array.Empty<string>();   
    }
}
