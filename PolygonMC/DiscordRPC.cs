/*
    PolygonMC - LFInteractive LLC. 2021-2024
    PolygonMC is a free and open source Minecraft Launcher implementing various modloaders, mod platforms, and minecraft authentication.
    PolygonMC is protected under GNU GENERAL PUBLIC LICENSE version 3.0 License
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
    https://github.com/DcmanProductions/PolygonMC
*/

using DiscordRPC;
using DiscordRPC.Logging;

namespace PolygonMC;

internal class DiscordRPC
{
    private static DiscordRPC instance;
    private DiscordRpcClient client;

    private DiscordRPC()
    {
        instance = this;
        client = new("1141071009101914202")
        {
            Logger = new ConsoleLogger() { Level = LogLevel.Trace, Coloured = true },
        };
        client.OnReady += (s, e) =>
        {
            Log.Information("[DISCORD RPC] Initializing Discord RPC for user {USER}", e.User.Username);
        };
        client.OnPresenceUpdate += (s, e) =>
        {
            Log.Information("[DISCORD RPC] Updating presence: {STATUS}", e.Presence);
        };

        client.Initialize();
        SetPresence("Loading...");
    }

    public static void SetPresence(string presence)
    {
        instance.client.SetPresence(new RichPresence()
        {
            Details = presence,
        });
    }

    public static void InitializeDiscordRPC()
    {
        instance ??= new DiscordRPC();
    }
}