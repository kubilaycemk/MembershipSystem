using System.ComponentModel.DataAnnotations;

namespace MembershipSystem.ViewModels
{
    public class SignInViewModel
    {
        [EmailAddress(ErrorMessage = "Email formatı yanlıştır.")]
        [Required(ErrorMessage = "Email alanı zorunludur.")]
        [Display(Name = "Email :")]
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        [Display(Name = "Şifre :")]
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }
    }
}
