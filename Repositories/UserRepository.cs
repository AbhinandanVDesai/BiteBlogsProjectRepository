using BiteBlogs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BiteBlogs.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext authDbContext;

        public UserRepository(AuthDbContext authDbContext)
        {
            this.authDbContext = authDbContext;
        }


        public async Task<IEnumerable<IdentityUser>> GetAllUsers()
        {
              var ListOfUsers = await authDbContext.Users.ToListAsync();

              var superAdmin= await authDbContext.Users.FirstOrDefaultAsync(x=>x.Email== "superadmin@biteblogs.com");

              if(superAdmin != null)
            {
                ListOfUsers.Remove(superAdmin);

            }
             
            
            return ListOfUsers;
        }

    }
}
