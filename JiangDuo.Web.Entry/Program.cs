using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args).Inject();
//builder.Host.UseSerilogDefault(config =>
//{
//    config.WriteTo
//        .Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
//        .WriteTo.File("logs/log.log", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true,
//            fileSizeLimitBytes: 4194304, restrictedToMinimumLevel: LogEventLevel.Error);
//});
//builder.Services.AddFileLogging("logs/application-{0:yyyy}-{0:MM}-{0:dd}.log", options =>
//{
//    options.FileNameRule = fileName =>
//    {
//        return string.Format(fileName, DateTime.UtcNow);
//    };
//});

Array.ForEach(new[] { LogLevel.Information, LogLevel.Warning, LogLevel.Error }, logLevel =>
{
    builder.Services.AddFileLogging($"logs/{logLevel}/{{0:yyyy}}-{{0:MM}}-{{0:dd}}.log", options =>
    {
        options.FileNameRule = fileName => string.Format(fileName, DateTime.UtcNow);
        options.WriteFilter = logMsg => logMsg.LogLevel == logLevel;
    });
});

var app = builder.Build();

app.Run();