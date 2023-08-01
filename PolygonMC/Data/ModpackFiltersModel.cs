/*
    PolygonMC - LFInteractive LLC. 2021-2024
    PolygonMC is a free and open source Minecraft Launcher implementing various modloaders, mod platforms, and minecraft authentication.
    PolygonMC is protected under GNU GENERAL PUBLIC LICENSE version 3.0 License
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
    https://github.com/DcmanProductions/PolygonMC
*/

namespace PolygonMC.Models;

public sealed class ModpackFiltersModel
{
    public bool ShowCurseForge { get; set; }
    public bool ShowModrinth { get; set; }
    public bool ShowFabric { get; set; }
    public bool ShowForge { get; set; }
    public bool ShowQuilt { get; set; }
    public List<string> Categories { get; set; } = new List<string>();
}