namespace MembershipSystem.Services
{
    public interface IEmailService
    {
        Task SendResetPasswordEmailAsync(string resetPasswordEmailLink, string toEmail);
    }
}
