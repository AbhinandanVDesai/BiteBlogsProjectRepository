using System.ComponentModel.DataAnnotations;

namespace BiteBlogs.Models.NewFolder
{
    public class RegisterRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(6,ErrorMessage ="Password must be atleast 6 character ")]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
