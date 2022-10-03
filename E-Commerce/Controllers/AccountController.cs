using E_Commerce.Data;
using E_Commerce.Data.ViewModel;
using E_Commerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using E_Commerce.Data.Static;
using Microsoft.Extensions.DependencyInjection;


namespace E_Commerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly E_CommerceDbContext _context;
        private readonly UserManager<ApplicationUser> _USER;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(E_CommerceDbContext context, UserManager<ApplicationUser> user , SignInManager<ApplicationUser> signInManager)
        {
                _context = context;
            _USER = user;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            var result = new LoginVM();
            return View(result);
        }
        public async Task<IActionResult> Users()
        {
            var Resposne = await _context.Users.ToListAsync();
            return View(Resposne);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _USER.FindByEmailAsync(model.EmailAddress);
            if (user != null)
            {
                //Check Password
                var passwordCheck = await _USER.CheckPasswordAsync(user, model.Password);
                if (passwordCheck==true)
                {
                    var Result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false); //first false for saving pass second false for block if he mistake in error
                    if (Result.Succeeded)
                    {
                        return RedirectToAction("Index", "Products");
                    }
                }
                return View(model);
            }
            return View(model);
        }
        public IActionResult Register()
        {
            var result = new RegisterVM();
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {

            var user = await _USER.FindByEmailAsync(model.EmailAddress);
            if (user != null)
            {
                return View(model);
            }
            var newUser = new ApplicationUser() { Email = model.EmailAddress, FullName = model.FullName, UserName = model.EmailAddress.Split('@')[0] };
            var Result = await _USER.CreateAsync(newUser, model.Password);
            if (Result.Succeeded)
            {
                await _USER.AddToRoleAsync(newUser,UserRoles.User);
            }
            return View("CompleteRegister");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Products");
        }


    }
}
