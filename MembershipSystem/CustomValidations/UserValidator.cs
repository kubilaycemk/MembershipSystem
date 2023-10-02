using MembershipSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace MembershipSystem.CustomValidations
{
    public class UserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            var errors = new List<IdentityError>();
            var isDigit = int.TryParse(user.UserName![0].ToString(), out _);
            if (isDigit)
            {
                errors.Add(new IdentityError()
                {
                    Code = "UserNameStartWithDigit",
                    Description = "Kullacı Adı sayısal karakter ile başlayamaz."
                });
            }
            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
