using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args).Inject();
builder.Host.UseSerilogDefault(config =>
{
	config.WriteTo
		.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
		.WriteTo.File("logs/log.log", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true,
			fileSizeLimitBytes: 4194304, restrictedToMinimumLevel: LogEventLevel.Error);
});
var app = builder.Build();

app.Run();