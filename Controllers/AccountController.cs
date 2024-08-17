using BiteBlogs.Models.NewFolder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BiteBlogs.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> UserManager,
            SignInManager<IdentityUser> SignInManager)
        {
            userManager = UserManager;
            signInManager = SignInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest RegisterData)
        {
            if (ModelState.IsValid)
            {



                var IdentityUser = new IdentityUser
                {

                    UserName = RegisterData.UserName,
                    Email = RegisterData.Email,

                };

                var IdentityResult = await userManager.CreateAsync(IdentityUser, RegisterData.Password);

                if (IdentityResult.Succeeded)
                {
                    var RoleIdentityUser = await userManager.AddToRoleAsync(IdentityUser, "user");

                    if (RoleIdentityUser.Succeeded)
                    {
                        //show success notification
                        TempData["Message"]=$"{RegisterData.UserName} you have Sucssecfully Registered";
                        return RedirectToAction("Index", "Home");

                    }

                }

            }
            //show error notification
            return View();

        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest LoginData)
        {
            var SignInResult= await signInManager.PasswordSignInAsync(LoginData.UserName, LoginData.Password,false,false);

             if(SignInResult!=null && SignInResult.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
             //show error
            return View();
        }

        [HttpGet]
            public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }




        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
