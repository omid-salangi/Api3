using Data;
using NLog;
using Microsoft.EntityFrameworkCore;
var logger = NLog.LogManager.Setup().LoadConfigurationFromFile("/nlog.config").GetCurrentClassLogger();
logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args);

    // add mvc to project
    builder.Services.AddControllersWithViews();

    builder.Services.AddDbContext<Context>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("BlogDbConnection"));
    });

    var app = builder.Build();





    app.UseStaticFiles();
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");


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
   NLog.LogManager.Shutdown();
}
