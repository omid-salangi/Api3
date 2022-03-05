using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.SiteSettings;
using Common.TranslatePersian;
using Domain.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Data;
using Microsoft.AspNetCore.Identity;

namespace WebFramework.Configuration
{
    public static class IdentityConfiguration
    {
        public static void AddCustomIdentity(this IServiceCollection services , SiteSettings _siteSettings)
        {
            services.AddIdentity<User,Roles>(options =>
            {
                // password
                options.Password.RequireDigit = _siteSettings.IdentitySettings.PasswordRequireDigit;
                options.Password.RequiredLength = _siteSettings.IdentitySettings.PasswordRequiredLength;
                // username
                options.User.RequireUniqueEmail= _siteSettings.IdentitySettings.RequireUniqueEmail;
                // sign in  cookie
                //options.SignIn.RequireConfirmedEmail = false;
                // lock out for for cookie and signinmanager
                //options.Lockout.MaxFailedAccessAttempts = 5;
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

            }).AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<TranslatePersian>();
        }
    }
}
