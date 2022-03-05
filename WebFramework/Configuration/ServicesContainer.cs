using System.Net;
using System.Security.Claims;
using System.Text;
using Application.Interface;
using Application.Profiles;
using Application.Services;
using Common.Api;
using Common.Exceptions;
using Data.Repositories;
using Domain.Interface;
using Domain.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Common;
using Common.SiteSettings;
using Common.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Webframework.Configuration
{
    public static class ServicesExtention
    {
        public static void AddDependencyInjections(this IServiceCollection services)
        {
            // service layer
            services.AddScoped<IPostService, PostServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IJwtServices, JwtServices>();

            //data layer
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUserRepository, UserRepository>();


        }

        public static void AddHsts(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
        }

      

    }

}
