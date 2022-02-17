using Common.SiteSettings;
using Data;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using NLog;
using Webframework.Configuration;
using WebFramework.Configuration;
using WebFramework.Middlewares;

var logger = LogManager.Setup().LoadConfigurationFromFile("/nlog.config").GetCurrentClassLogger();
logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args);
    // site settings
    var _sitesettings = builder.Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

    builder.Services.Configure<SiteSettings>(builder.Configuration.GetSection(nameof(SiteSettings)));
    // add mvc to project
    builder.Services.AddControllersWithViews();
    builder.Services.AddMvcCore(option =>
    {
        option.Filters.Add(new AuthorizeFilter()); // authorize for all of controllers 
    });

    builder.Services.AddDbContext<Context>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreDbConnection"));
    });
    builder.Services.AddCustomIdentity(_sitesettings);
    builder.Services.AddDependencyInjections();
    builder.Services.AddJwtAuthentication(_sitesettings.JwtSettings);

    var app = builder.Build();


    app.UseCustomExceptionHandler();

    //if (!app.Environment.IsDevelopment())
    //{
    //    app.UseExceptionHandler("/Home/Error");
    //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //    app.UseHsts();
    //}

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    
    app.MapControllerRoute(
        "default",
        "{controller=Home}/{action=Index}/{id?}");


    app.Run();
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    LogManager.Shutdown();
}