/*
    PolygonMC - LFInteractive LLC. 2021-2024
    PolygonMC is a free and open source Minecraft Launcher implementing various modloaders, mod platforms, and minecraft authentication.
    PolygonMC is protected under GNU GENERAL PUBLIC LICENSE version 3.0 License
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
    https://github.com/DcmanProductions/PolygonMC
*/

global using static PolygonMC.Data.Constants;
using Chase.Minecraft.Instances;
using Newtonsoft.Json;
using System.Reflection;

namespace PolygonMC.Data;

internal enum InstanceSortMethod
{
    [JsonProperty("last-played")]
    LastPlayed,

    [JsonProperty("play-time")]
    PlayTime,

    [JsonProperty("number-of-plays")]
    NumberOfPlays,

    [JsonProperty("alphabetically")]
    Alphabetically,
}

public static class Constants
{
    public static string ApplicationName { get; } = "PolygonMC";
    public static AssemblyName ApplicationAssembly { get; } = Assembly.GetExecutingAssembly().GetName();
    public static Version ApplicationVersion { get; } = ApplicationAssembly.Version;
    public static string ApplicationDirectory { get; } = Directory.GetParent(Assembly.GetExecutingAssembly().Location ?? "").FullName;
    public static string MinecraftClientID { get; } = "f8b88f7d-77d7-49ca-9b97-5bb12a4ee48f";
    public static string MicrosoftRedirectURI { get; } = "http://127.0.0.1:56748";
    public static string MSAFile { get; } = Path.Combine(Configuration.Instance.WorkingDirectory, "msa-auth.json");
    public static InstanceManager InstanceManager { get; } = new(Path.Combine(Configuration.Instance.WorkingDirectory, "instances"));
    public static bool IsAuthenticated => Configuration.Instance.IsAuthenticated;
    public static string AuthenticationToken { get; set; } = "";
    public static string JavaDirectory => Path.Combine(Configuration.Instance.WorkingDirectory, "java");
    public static string[] Themes => Directory.GetFiles(Path.Combine(ApplicationDirectory, "wwwroot", "css", "themes"), "*.css", SearchOption.TopDirectoryOnly).Select(i => Path.GetFileName(i).Replace(Path.GetExtension(i), "")).ToArray();

    public static string FormatNumber(byte number) => FormatNumber((int)number);

    public static string FormatNumber(short number) => FormatNumber((int)number);

    public static string FormatNumber(int number) => FormatNumber((double)number);

    public static string FormatNumber(float number) => FormatNumber((double)number);

    public static string FormatNumber(double number)
    {
        string[] suffixes = { "", "K", "M", "B", "T" };
        int magnitude = 0;
        double value = number;

        while (value >= 1000 && magnitude < suffixes.Length - 1)
        {
            magnitude++;
            value /= 1000;
        }

        return $"{value:0.##}{suffixes[magnitude]}";
    }
}