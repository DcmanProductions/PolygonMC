/*
    PolygonMC - LFInteractive LLC. 2021-2024
    PolygonMC is a free and open source Minecraft Launcher implementing various modloaders, mod platforms, and minecraft authentication.
    PolygonMC is protected under GNU GENERAL PUBLIC LICENSE version 3.0 License
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
    https://github.com/DcmanProductions/PolygonMC
*/

global using static PolygonMC.Data.Constants;
using Chase.Minecraft.Instances;
using System.Reflection;

namespace PolygonMC.Data;

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
    public static bool IsAuthenticated { get; set; } = false;
    public static string AuthenticationToken { get; set; } = "";
    public static string JavaDirectory => Path.Combine(Configuration.Instance.WorkingDirectory, "java");
}