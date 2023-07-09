using System.ComponentModel.DataAnnotations;

namespace MembershipSystem.ViewModels
{
    public class SignUpViewModel
    {
        [Display(Name = "Kullanıcı Adı :")]
        public string UserName { get; set; }

        [Display(Name = "Email :")]
        public string Email { get; set; }

        [Display(Name = "Telefon :")]
        public string Phone { get; set; }

        [Display(Name = "Şifre :")]
        public string Password { get; set; }

        [Display(Name = "Şifre Tekrar :")]
        public string PasswordConfirm { get; set; }
    }
}
