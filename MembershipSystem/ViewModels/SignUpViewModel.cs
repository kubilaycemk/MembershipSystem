using System.ComponentModel.DataAnnotations;

namespace MembershipSystem.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
        [Display(Name = "Kullanıcı Adı :")]
        public string UserName { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Email formatı yanlıştır.")]
        [Required(ErrorMessage = "Email alanı zorunludur.")]
        [Display(Name = "Email :")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Telefon alanı zorunludur.")]
        [Display(Name = "Telefon :")]
        public string? Phone { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        [Display(Name = "Şifre :")]
        [MinLength(6, ErrorMessage = "Şifreniz en az 6 karakter olmalıdır.")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Şifreler eşleşmedi.")]
        [Required(ErrorMessage = "Şifre Tekrar zorunludur.")]
        [Display(Name = "Şifre Tekrar :")]
        public string PasswordConfirm { get; set; } = null!;
    }
}
