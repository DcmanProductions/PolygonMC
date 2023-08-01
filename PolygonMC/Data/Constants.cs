/*
    PolygonMC - LFInteractive LLC. 2021-2024
    PolygonMC is a free and open source Minecraft Launcher implementing various modloaders, mod platforms, and minecraft authentication.
    PolygonMC is protected under GNU GENERAL PUBLIC LICENSE version 3.0 License
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
    https://github.com/DcmanProductions/PolygonMC
*/

global using static PolygonMC.Data.Constants;
using System.Reflection;

namespace PolygonMC.Data;

public static class Constants
{
    public static readonly string ApplicationName = "PolygonMC";
    public static readonly AssemblyName ApplicationAssembly = Assembly.GetExecutingAssembly().GetName();
    public static readonly Version ApplicationVersion = ApplicationAssembly.Version;
    public static readonly string ApplicationDirectory = Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName;
    public static readonly string MinecraftClientID = "f8b88f7d-77d7-49ca-9b97-5bb12a4ee48f";
    public static readonly string MicrosoftRedirectURI = "http://127.0.0.1:56748";
    public static readonly string MSAFile = Path.Combine(ApplicationDirectory, "msa-auth.json");
    public static bool IsAuthenticated = false;
    public static string AuthenticationToken = "";
}