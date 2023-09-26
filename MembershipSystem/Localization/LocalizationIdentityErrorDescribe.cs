using Microsoft.AspNetCore.Identity;

namespace MembershipSystem.Localization
{
    public class LocalizationIdentityErrorDescribe:IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError { Code = "DuplicateUserName", Description = $"Bu {userName} başka bir kullanıcı tarafından alınmıştır." };
        }
    }
}
