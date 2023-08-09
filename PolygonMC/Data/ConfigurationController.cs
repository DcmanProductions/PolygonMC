/*
    PolygonMC - LFInteractive LLC. 2021-2024
    PolygonMC is a free and open source Minecraft Launcher implementing various modloaders, mod platforms, and minecraft authentication.
    PolygonMC is protected under GNU GENERAL PUBLIC LICENSE version 3.0 License
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
    https://github.com/DcmanProductions/PolygonMC
*/

using Chase.Minecraft.Data;
using Chase.Minecraft.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace PolygonMC.Data;

internal sealed class ConfigurationController
{
    [JsonIgnore]
    public static ConfigurationController Instance = new();

    [JsonProperty("theme")]
    public string Theme = "default";

    [JsonIgnore]
    private readonly string configFile;

    [JsonProperty("setup")]
    public bool HasSetup { get; set; } = false;

    [JsonProperty("directory")]
    public string WorkingDirectory { get; set; } = Directory.GetParent(Assembly.GetExecutingAssembly().Location ?? "").FullName;

    [JsonProperty("username")]
    public string Username { get; set; } = "";

    [JsonProperty("profile")]
    public UserProfile Profile { get; set; } = new();

    [JsonProperty("curseforge-api")]
    public string CurseForgeAPI { get; set; } = "";

    [JsonProperty("modrinth-api")]
    public string ModrinthAPI { get; set; } = "";

    [JsonProperty("has-downloaded-java")]
    public bool HasDownloadedJava { get; set; } = false;

    [JsonProperty("window-width")]
    public int WindowWidth { get; set; } = 800;

    [JsonProperty("window-height")]
    public int WindowHeight { get; set; } = 480;

    [JsonProperty("ram")]
    public RAMInfo RAM { get; set; } = new RAMInfo();

    [JsonProperty("default-platform")]
    public PlatformSource DefaultPlatform { get; set; } = PlatformSource.Modrinth;

    [JsonIgnore]
    public bool IsAuthenticated { get; set; } = false;

    private ConfigurationController()
    {
        Instance = this;
        configFile = Path.Combine(ApplicationDirectory, "settings.json");
    }

    public void Load()
    {
        if (!File.Exists(configFile))
        {
            Save();
        }
        try
        {
            Instance = JObject.Parse(File.OpenText(configFile).ReadToEnd())?.ToObject<ConfigurationController>() ?? Instance;
        }
        catch (Exception e)
        {
            Log.Error("Unable to load config", e);
        }
    }

    public void Save(int attmpt = 0)
    {
        try
        {
            using FileStream fs = new(configFile, FileMode.Create, FileAccess.Write, FileShare.Read);
            using StreamWriter writer = new(fs);
            writer.Write(JsonConvert.SerializeObject(this, Formatting.Indented));
        }
        catch (Exception e)
        {
            Log.Error("Unable to save config", e);
            try
            {
                File.Delete(configFile);
                if (attmpt < 10)
                {
                    Thread.Sleep(100);
                    Save(attmpt + 1);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Unable to delete config file.", ex);
            }
        }
    }
}