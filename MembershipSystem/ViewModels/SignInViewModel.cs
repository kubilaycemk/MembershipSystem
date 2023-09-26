using System.ComponentModel.DataAnnotations;

namespace MembershipSystem.ViewModels
{
    public class SignInViewModel
    {
        [EmailAddress(ErrorMessage = "Email formatı yanlıştır.")]
        [Required(ErrorMessage = "Email alanı zorunludur.")]
        [Display(Name = "Email :")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        [Display(Name = "Şifre :")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
