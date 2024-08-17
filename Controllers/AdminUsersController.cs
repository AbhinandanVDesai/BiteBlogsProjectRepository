using BiteBlogs.Models.NewFolder;
using BiteBlogs.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BiteBlogs.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AdminUsersController(IUserRepository userRepository,
                                   UserManager<IdentityUser> UserManager,
                                   SignInManager<IdentityUser> SignInManager
            )
        {
            this.userRepository = userRepository;
            userManager = UserManager;
            signInManager = SignInManager;
        }


        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            var UserListDomain = await userRepository.GetAllUsers();

            var UserListViewModel = new UserListViewModel();

            UserListViewModel.UserList = new List<User>();

            foreach (var user in UserListDomain)
            {
                UserListViewModel.UserList.Add(new Models.NewFolder.User
                {
                    Id = Guid.Parse(user.Id),
                    UserName = user.UserName,
                    UserEmail = user.Email
                });

            }
            return View("List", UserListViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> UserList(UserListViewModel createUserRequest)
        {
            var identityUser = new IdentityUser
            {
                UserName = createUserRequest.UserName,
                Email = createUserRequest.Email,
            };

            var identityResult = await userManager.CreateAsync(identityUser, createUserRequest.Password);

            if (identityResult.Succeeded)
            {
                var roles = new List<string> { "User" };

                if (createUserRequest.AdminRoleCheckBox)
                {
                    roles.Add("Admin");
                }

                foreach (var role in roles)
                {
                    var roleResult = await userManager.AddToRoleAsync(identityUser, role);
                    if (!roleResult.Succeeded)
                    {
                        // Handle role addition failure (optional)
                    }
                }

                return RedirectToAction("UserList");
            }

            // Handle user creation failure
            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(createUserRequest);
        }






        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var DelUser= await userManager.FindByIdAsync(id.ToString());

            if(DelUser != null)
            {
               var identityResult= await userManager.DeleteAsync(DelUser);   

                if(identityResult.Succeeded)
                {
                    return RedirectToAction("UserList", "AdminUsers");
                }
            }

            return View("List");

        }
        

    }
}
