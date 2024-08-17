using System.ComponentModel.DataAnnotations;

namespace BiteBlogs.Models.NewFolder
{
    public class LoginRequest
    {

        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
