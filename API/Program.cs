using Common.SiteSettings;
using Data;
using Microsoft.EntityFrameworkCore;
using NLog;
using WebFramework.AutoMapperConfiguration;
using Webframework.Configuration;
using WebFramework.Configuration;
using WebFramework.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var logger = LogManager.Setup().LoadConfigurationFromFile("/nlog.config").GetCurrentClassLogger();
logger.Debug("init main");
try
{

// Add services to the container.
    var _sitesettings = builder.Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
    builder.Services.Configure<SiteSettings>(builder.Configuration.GetSection(nameof(SiteSettings)));
    builder.Services.AddJwtAuthentication(_sitesettings.JwtSettings);
    builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<Context>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreDbConnection"));
    });
    builder.Services.AddCustomIdentity(_sitesettings);
    builder.Services.AddJwtAuthentication(_sitesettings.JwtSettings);

    // auto mapper 
    builder.Services.AddAutoMappers();
    //builder.Services.AddDependencyInjections();
    //autofac
    builder.BuildAutofacServiceProvider();

    var app = builder.Build();
    app.UseCustomExceptionHandler();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

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