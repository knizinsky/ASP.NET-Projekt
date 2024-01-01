using GroceryStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(ApplicationUser model)
        {
            if(model.UserName == null)
            {
                ModelState.AddModelError(string.Empty, "Pole nazwa użytkownika jest wymagane.");
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                System.Diagnostics.Debug.WriteLine(user, model.Password);
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe = false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _httpContextAccessor.HttpContext.Session.SetString("UserName", user.UserName);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Nieprawidłowe dane logowania.");
                }
            }

            ModelState.AddModelError(string.Empty, "Nieprawidłowe dane logowania.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }

}




