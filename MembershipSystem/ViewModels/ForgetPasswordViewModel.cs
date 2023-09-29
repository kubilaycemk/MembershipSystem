using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MembershipSystem.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email alanı boş olamaz.")]
        [EmailAddress(ErrorMessage ="Email formatı hatalı.")]
        [Display(Name ="Email :")]
        public string Email { get; set; }     
    }
}
