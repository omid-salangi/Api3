using NLog;
var logger = NLog.LogManager.Setup().LoadConfigurationFromFile("/nlog.config").GetCurrentClassLogger();
logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args);


    var app = builder.Build();

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
