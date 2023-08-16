/*
    PolygonMC - LFInteractive LLC. 2021-2024
    PolygonMC is a free and open source Minecraft Launcher implementing various modloaders, mod platforms, and minecraft authentication.
    PolygonMC is protected under GNU GENERAL PUBLIC LICENSE version 3.0 License
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
    https://github.com/DcmanProductions/PolygonMC
*/

using Chase.Minecraft.Controller;
using Microsoft.Extensions.Logging;
using PolygonMC.Data;
using Serilog.Formatting.Json;
using System.IO.Pipes;

namespace PolygonMC;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        ConfigurationController.Instance.Load();
        string logsDirectory = Directory.CreateDirectory(Path.Combine(ConfigurationController.Instance.WorkingDirectory, "logs")).FullName;
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

        AppDomain.CurrentDomain.ProcessExit += (s, e) =>
        {
            Log.CloseAndFlush();
        };
        Log.Debug("Starting PolygonMC - {VER}", ApplicationVersion);

        using (Mutex mutex = new(true, ApplicationName, out bool createNew))
        {
            if (createNew)
            {
                Thread pipeThread = new(ReceiveArgumentsFromNewInstance)
                {
                    IsBackground = true,
                };
                pipeThread.Start();
            }
            else
            {
                SendArgumentsToRunningInstance();
            }
        }

        Task.Run(() =>
        {
            if (ConfigurationController.Instance.Profile.Skins != null || !ConfigurationController.Instance.Profile.Skins.Any() || ConfigurationController.Instance.Profile.Skins.First().Url != null)
            {
                try
                {
                    UserProfileController.GetFace(ConfigurationController.Instance.Profile, true);
                }
                catch (Exception ex)
                {
                    Log.Error("Unable to get user face from texture: {PROFILE}", ConfigurationController.Instance.Profile, ex);
                }
            }
        });

        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddSerilog(Log.Logger, dispose: true);

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        DiscordRPC.InitializeDiscordRPC();

        return builder.Build();
    }

    private static void SendArgumentsToRunningInstance()
    {
        try
        {
            using NamedPipeClientStream pipeClient = new(".", ApplicationName, PipeDirection.Out);
            pipeClient.Connect(TimeSpan.FromSeconds(30));
            using StreamWriter writer = new(pipeClient);
            foreach (string arg in Environment.GetCommandLineArgs())
            {
                writer.WriteLine(arg);
            }
        }
        catch (Exception e)
        {
            Log.Error("Unable to send arguments to running instance", e);
        }
    }

    private static void ReceiveArgumentsFromNewInstance()
    {
        using NamedPipeServerStream pipeServer = new(ApplicationName, PipeDirection.In);
        while (true)
        {
            try
            {
                //var connectionHandle = pipeServer.BeginWaitForConnection(null, null);

                //pipeServer.EndWaitForConnection(connectionHandle);
                pipeServer.WaitForConnection();
                using StreamReader reader = new(pipeServer);
                string args = reader.ReadLine();
                Log.Information(args);
                pipeServer.Disconnect();
            }
            catch (Exception e)
            {
                Log.Error("Unable to receive arguments to running instance", e);
            }
        }
    }
}