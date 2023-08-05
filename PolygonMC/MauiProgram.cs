/*
    PolygonMC - LFInteractive LLC. 2021-2024
    PolygonMC is a free and open source Minecraft Launcher implementing various modloaders, mod platforms, and minecraft authentication.
    PolygonMC is protected under GNU GENERAL PUBLIC LICENSE version 3.0 License
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
    https://github.com/DcmanProductions/PolygonMC
*/

using Microsoft.Extensions.Logging;
using PolygonMC.Data;
using Serilog.Formatting.Json;

namespace PolygonMC;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        Configuration.Instance.Load();
        string logsDirectory = Directory.CreateDirectory(Path.Combine(Configuration.Instance.WorkingDirectory, "logs")).FullName;
        string template = @"[PolygonMC] {Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}";
        TimeSpan dumpLogInterval = TimeSpan.FromSeconds(5);

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose, outputTemplate: template)
            .WriteTo.Debug(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose, outputTemplate: template)
            .WriteTo.File(Path.Combine(logsDirectory, "debug.log"), restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose, outputTemplate: template, rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 15_000_000, buffered: true, flushToDiskInterval: dumpLogInterval)
            .WriteTo.File(Path.Combine(logsDirectory, "latest.log"), restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information, outputTemplate: template, rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 15_000_000, buffered: true, flushToDiskInterval: dumpLogInterval)
            .WriteTo.File(Path.Combine(logsDirectory, "error.log"), restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error, outputTemplate: template, rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 15_000_000, buffered: true, flushToDiskInterval: dumpLogInterval)
            .WriteTo.File(new JsonFormatter(), Path.Combine(logsDirectory, "error.json"), Serilog.Events.LogEventLevel.Error, fileSizeLimitBytes: 15_000_000, rollingInterval: RollingInterval.Day, buffered: false)
            .CreateLogger();

        Log.Debug("Starting PolygonMC");

        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddSerilog(Log.Logger, dispose: true);

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        AppDomain.CurrentDomain.ProcessExit += (s, e) =>
        {
            Log.CloseAndFlush();
        };

        return builder.Build();
    }
}