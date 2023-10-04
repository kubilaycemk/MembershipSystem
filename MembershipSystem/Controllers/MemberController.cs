using MembershipSystem.Extensions;
using MembershipSystem.Models;
using MembershipSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;

namespace MembershipSystem.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFileProvider _fileProvider;

        public MemberController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IFileProvider fileProvider)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _fileProvider = fileProvider;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name!);

            var userViewModel = new UserViewModel {
                Email = currentUser.Email,
                PhoneNumber = currentUser.PhoneNumber,
                UserName = currentUser.UserName,
                PictureUrl = currentUser.Picture
            };


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

                ModelState.AddModelErrorList(result.Errors);

                return View();
            }

            await _userManager.UpdateSecurityStampAsync(currentUser);
            await _signInManager.SignOutAsync();
            await _signInManager.PasswordSignInAsync(currentUser, request.PasswordNew,true,false);


            TempData["SuccessMessage"] = "Şifreniz başarıyla değiştirildi.";

            return View();

        }

        public async Task<IActionResult> UserEdit()
        {
            ViewBag.genderList = new SelectList (Enum.GetNames (typeof (Gender)));
            var currentUser = await _userManager.FindByNameAsync (User.Identity!.Name!);

            var userEditModel = new UserEditViewModel()
            {
                UserName = currentUser.UserName,
                Email = currentUser.Email,
                Phone = currentUser.PhoneNumber,
                City = currentUser.City,
                BirthDate = currentUser.BirthDate,
                Gender = currentUser.Gender,
            };

            return View(userEditModel);
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(UserEditViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name!);

            currentUser.BirthDate = request.BirthDate;
            currentUser.Gender = request.Gender;
            currentUser.City = request.City;
            currentUser.PhoneNumber = request.Phone;



            if(request.Picture != null && request.Picture.Length > 0)
            {
                var wwwrootFolder = _fileProvider.GetDirectoryContents("wwwroot");

                var randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(request.Picture.FileName)}";

                var newPicture = Path.Combine(wwwrootFolder.First(x=> x.Name == "userpictures").PhysicalPath!, randomFileName);

                using var stream = new FileStream(newPicture, FileMode.Create);
                await request.Picture.CopyToAsync(stream);

                currentUser.Picture = randomFileName;
            }

            var updateUserResult =  await _userManager.UpdateAsync(currentUser);

            if (!updateUserResult.Succeeded)
            {
                ModelState.AddModelErrorList(updateUserResult.Errors);

                return View();
            }

            await _userManager.UpdateSecurityStampAsync(currentUser);
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(currentUser, true);


            var userEditModel = new UserEditViewModel()
            {
                UserName = currentUser.UserName,
                Email = currentUser.Email,
                Phone = currentUser.PhoneNumber,
                City = currentUser.City,
                BirthDate = currentUser.BirthDate,
                Gender = currentUser.Gender

            };

            TempData["SuccessMessage"] = "Bilgileriniz başarıyla değiştirildi.";
            return View(userEditModel);
        }
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
