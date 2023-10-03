using MembershipSystem.Extensions;
using MembershipSystem.Models;
using MembershipSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MembershipSystem.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public MemberController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name!);

            var userViewModel = new UserViewModel { Email = currentUser.Email, PhoneNumber = currentUser.PhoneNumber, UserName = currentUser.UserName };


            return View(userViewModel);
        }

        public  IActionResult PasswordChange()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name!);

            var checkOldPassword = await _userManager.CheckPasswordAsync(currentUser!, request.PasswordOld);

            if (!checkOldPassword)
            {
                ModelState.AddModelError(string.Empty, "Eski şifre yenlış.");
                return View();
            }

            var result = await _userManager.ChangePasswordAsync(currentUser,request.PasswordOld, request.PasswordNew);

            if(!result.Succeeded) {

                ModelState.AddModelErrorList(result.Errors.Select(x => x.Description).ToList());

                return View();
            }

            await _userManager.UpdateSecurityStampAsync(currentUser);
            await _signInManager.SignOutAsync();
            await _signInManager.PasswordSignInAsync(currentUser, request.PasswordNew,true,false);


            TempData["SuccessMessage"] = "Şifreniz başarıyla değiştirildi.";

            return View();

        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
