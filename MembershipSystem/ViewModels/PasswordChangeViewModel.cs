using System.ComponentModel.DataAnnotations;

namespace MembershipSystem.ViewModels
{
    public class PasswordChangeViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Eski şifre alanı zorunludur.")]
        [Display(Name = "Eski Şifre :")]
        public string PasswordOld { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Yeni şifre alanı zorunludur.")]
        [Display(Name = "Yeni Şifre :")]
        [MinLength(6, ErrorMessage = "Şifreniz en az 6 karakter olmalıdır.")]
        public string PasswordNew { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Yeni şifre tekrar alanı zorunludur.")]
        [Display(Name = "Yeni Şifre Tekrar :")]
        [Compare(nameof(PasswordNew),ErrorMessage ="Şifeler eşleşmiyor.")]
        public string PasswordConfirm { get; set; }

    }
}
