using System.Net;
using System.Security.Claims;
using System.Text;
using Application.Interface;
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

        
        
    }

}
