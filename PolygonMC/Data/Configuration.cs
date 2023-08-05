/*
    PolygonMC - LFInteractive LLC. 2021-2024
    PolygonMC is a free and open source Minecraft Launcher implementing various modloaders, mod platforms, and minecraft authentication.
    PolygonMC is protected under GNU GENERAL PUBLIC LICENSE version 3.0 License
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
    https://github.com/DcmanProductions/PolygonMC
*/

using Chase.Minecraft.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace PolygonMC.Data;

internal sealed class Configuration
{
    [JsonIgnore]
    public static Configuration Instance = new();

    [JsonIgnore]
    private readonly string configFile;

    [JsonProperty("directory")]
    public string WorkingDirectory { get; set; }

    [JsonProperty("username")]
    public string Username { get; set; }

    [JsonProperty("profile")]
    public UserProfile Profile { get; set; }

    private Configuration()
    {
        Instance = this;
        WorkingDirectory = Directory.GetParent(Assembly.GetExecutingAssembly().Location ?? "").FullName;
        Username = "";
        Profile = new UserProfile();
        configFile = Path.Combine(WorkingDirectory, "settings.json");
    }

    public void Load()
    {
        if (!File.Exists(configFile))
        {
            Save();
        }
        Instance = JObject.Parse(File.OpenText(configFile).ReadToEnd())?.ToObject<Configuration>() ?? Instance;
    }

    public void Save()
    {
        File.WriteAllText(configFile, JsonConvert.SerializeObject(this));
    }
}