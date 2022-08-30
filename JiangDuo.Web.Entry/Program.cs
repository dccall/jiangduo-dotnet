using Furion.Logging;
using Furion.Templates;
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
//builder.Services.AddFileLogging("logs/Information/{0:yyyy}-{0:MM}-{0:dd}.log", options =>
//{
//    options.FileNameRule = fileName =>
//    {
//        return string.Format(fileName, DateTime.UtcNow);
//    };
//    options.MinimumLevel = LogLevel.Information;
//});

Array.ForEach(new[] { LogLevel.Information, LogLevel.Warning, LogLevel.Error }, logLevel =>
{
    builder.Services.AddFileLogging($"logs/{logLevel}/{{0:yyyy}}-{{0:MM}}-{{0:dd}}.log", options =>
    {
        options.FileNameRule = fileName => string.Format(fileName, DateTime.UtcNow);
        options.WriteFilter = logMsg => logMsg.LogLevel == logLevel;
        options.HandleWriteError = (writeError) =>
        {
            writeError.UseRollbackFileName(Path.GetFileNameWithoutExtension(writeError.CurrentFileName) + "-oops" + Path.GetExtension(writeError.CurrentFileName));
        };
        //options.MessageFormat = (logMsg) =>
        // {
        //     return TP.Wrapper(logMsg.LogLevel, logMsg.Message,
        //"##��¼ʱ��## " + DateTime.Now.ToString("o"),
        //"##��־����## " + logMsg.LogLevel,
        //"##�̱߳��## " + logMsg.EventId.Id,
        //"##��ջ��Ϣ## " + logMsg.Exception?.ToString());
        // // ������д��
        // //return logMsg.WriteArray(writer =>
        // //        {
        // //            writer.WriteStringValue(DateTime.Now.ToString("o"));
        // //            writer.WriteStringValue(logMsg.LogLevel.ToString());
        // //            writer.WriteStringValue(logMsg.LogName);
        // //            writer.WriteNumberValue(logMsg.EventId.Id);
        // //            writer.WriteStringValue(logMsg.Message);
        // //            writer.WriteStringValue(logMsg.Exception?.ToString());
        // //        });
        //};
    });
});

var app = builder.Build();

app.Run();