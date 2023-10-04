using System.Reflection.Metadata.Ecma335;

namespace MembershipSystem.ViewModels
{
    public class UserViewModel
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? PictureUrl { get; set; }
    }
}
