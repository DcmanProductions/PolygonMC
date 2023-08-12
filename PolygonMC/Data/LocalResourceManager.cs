/*
    PolygonMC - LFInteractive LLC. 2021-2024
    PolygonMC is a free and open source Minecraft Launcher implementing various modloaders, mod platforms, and minecraft authentication.
    PolygonMC is protected under GNU GENERAL PUBLIC LICENSE version 3.0 License
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
    https://github.com/DcmanProductions/PolygonMC
*/

using Chase.Minecraft.Model;
using Chase.Minecraft.Modpacks.Model;
using Newtonsoft.Json;

namespace PolygonMC.Data;

public class LocalResourceManager
{
    [JsonProperty("resourcepacks")]
    private List<ResourcePack> ResourcePacks { get; set; }

    [JsonProperty("shaderpacks")]
    private List<ShaderPack> ShaderPacks { get; set; }

    [JsonProperty("worlds")]
    private List<World> Worlds { get; set; }

    public LocalResourceManager()
    {
        ResourcePacks = new List<ResourcePack>();
        ShaderPacks = new List<ShaderPack>();
        Worlds = new List<World>();
    }

    public void LoadResourcePack(InstanceModel instance, ResourcePack resourcePack)
    {
    }
}