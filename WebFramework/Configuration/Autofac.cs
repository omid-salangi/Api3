using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interface;
using Application.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Dependency;
using Common.SiteSettings;
using Data.Repositories;
using Domain.Interface;
using Domain.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebFramework.Configuration
{
    public static class AutofacExtentions
    {
        public static void BuildAutofacServiceProvider(this WebApplicationBuilder builder)
        {
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            // Register services directly with Autofac here. Don't
            // call builder.Populate(), that happens in AutofacServiceProviderFactory.
            builder.Host.ConfigureContainer<ContainerBuilder>(builders =>
            {
                builders.RegisterModule(new AutofacModule());
            });

        }
    }

    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            //// services
            //  builder.RegisterType<JwtServices>().As<IJwtServices>().InstancePerLifetimeScope();
            //builder.RegisterType<UserServices>().As<IUserServices>().InstancePerMatchingLifetimeScope();
            //builder.RegisterType<PostServices>().As<IPostService>().InstancePerMatchingLifetimeScope();

            //// repository 
            //builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().InstancePerMatchingLifetimeScope();
            //builder.RegisterType<PostRepository>().As<IPostRepository>().InstancePerMatchingLifetimeScope();
            //builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerMatchingLifetimeScope();

            // auto registration
            var commonAssembly = typeof(SiteSettings).Assembly; // any class we want in layer 
            var dataAssembly = typeof(PostRepository).Assembly;
            var domainAssembly = typeof(User).Assembly;
            var ApplicationAssembly = typeof(JwtServices).Assembly;

            builder.RegisterAssemblyTypes(commonAssembly, dataAssembly, domainAssembly, ApplicationAssembly)
                .AssignableTo<IScopedDependency>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(commonAssembly, dataAssembly, domainAssembly, ApplicationAssembly)
                .AssignableTo<ITransientDependency>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterAssemblyTypes(commonAssembly, dataAssembly, domainAssembly, ApplicationAssembly)
                .AssignableTo<IsingleDependency>().AsImplementedInterfaces().SingleInstance();
            base.Load(builder);
        }
    }
}