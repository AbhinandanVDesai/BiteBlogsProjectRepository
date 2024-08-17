using Microsoft.AspNetCore.Identity;

namespace BiteBlogs.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAllUsers();
    }
}
