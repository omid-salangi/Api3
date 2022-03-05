using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Application.Model;
using Application.Profiles;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;


namespace WebFramework.AutoMapperConfiguration
{
    public static class AutoMapperConfiguration
    {
        public static void AddAutoMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

        }

    }
}
