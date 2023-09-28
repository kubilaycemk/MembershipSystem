using MembershipSystem.Models;
using MembershipSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MembershipSystem.Extensions;

namespace MembershipSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model,string? returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Action("Index", "Home");

            var hasUser = await _userManager.FindByEmailAsync(model.Email);

            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Email veya Şifre yanlış");
                return View();
            }
            var singinResult = await _signInManager.PasswordSignInAsync(hasUser, model.Password, model.RememberMe, true);

            if (singinResult.Succeeded)
            {
                return Redirect(returnUrl);
            }

            if(singinResult.IsLockedOut) {
                ModelState.AddModelError(string.Empty, "3 dakika boyunca giriş yapamazsınız.");
                return View();
            }

            ModelState.AddModelErrorList(new List<string>() { "Email veya Şifre yanlış." });

            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var identityResult = await _userManager.CreateAsync(new() { UserName = request.UserName, Email = request.Email, PhoneNumber = request.Phone }, request.Password);

            if (identityResult.Succeeded)
            {
                TempData["SuccessMessage"] = "Üyelik kayıt işlemi başarıyla gerçekleşmiştir.";
                return RedirectToAction(nameof(HomeController.SignUp));
            }

            ModelState.AddModelErrorList(identityResult.Errors.Select(x => x.Description).ToList());

            //foreach (var item in identityResult.Errors)
            //{
            //    ModelState.AddModelError(string.Empty, item.Description);
            //}

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}