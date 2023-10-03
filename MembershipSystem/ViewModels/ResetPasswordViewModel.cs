using System.ComponentModel.DataAnnotations;

namespace MembershipSystem.ViewModels
{
    public class ResetPasswordViewModel
    {

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        [Display(Name = "Yeni Şifre :")]
        [MinLength(6, ErrorMessage = "Şifreniz en az 6 karakter olmalıdır.")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Şifreler eşleşmedi.")]
        [Required(ErrorMessage = "Şifre Tekrar zorunludur.")]
        [Display(Name = "Yeni Şifre Tekrar :")]
        public string PasswordConfirm { get; set; } = null!;
    }
}
