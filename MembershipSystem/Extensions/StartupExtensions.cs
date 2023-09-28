using MembershipSystem.Contexts;
using MembershipSystem.CustomValidations;
using MembershipSystem.Models;

namespace MembershipSystem.Extensions
{
    public static class StartupExtensions
    {
        public static void AddIdentityWithExtension(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnoprstuvwxyz0123456789";

                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                options.Lockout.MaxFailedAccessAttempts = 3;

            }).AddPasswordValidator<PasswordValidator>()
            .AddUserValidator<UserValidator>()
            .AddEntityFrameworkStores<AppDbContext>();
        }

        public static void AddCookieWithExtension(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(opt =>
            {
                var cookieBuilder = new CookieBuilder();
                cookieBuilder.Name = "MembershipSystemCookie";
                opt.Cookie = cookieBuilder;
                opt.LogoutPath = new PathString("/Member/Logout");
                opt.LoginPath = new PathString("/Home/Signin");
                opt.ExpireTimeSpan = TimeSpan.FromDays(60);
                opt.SlidingExpiration = true;
            });
        }
    }
}
