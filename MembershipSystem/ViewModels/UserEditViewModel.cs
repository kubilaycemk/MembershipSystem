using MembershipSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace MembershipSystem.ViewModels
{
    public class UserEditViewModel
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
        public string Phone { get; set; } = null!;

        [Display(Name = "Şehir :")]
        public string? City { get; set; }

        [Display(Name = "Fotoğraf :")]
        public IFormFile? Picture { get; set; }

        [Display(Name = "Doğum Tarihi :")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Cinsiyet :")]
        public Gender? Gender { get; set; }

    }
}
