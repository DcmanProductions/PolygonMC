/*
    PolygonMC - LFInteractive LLC. 2021-2024
    PolygonMC is a free and open source Minecraft Launcher implementing various modloaders, mod platforms, and minecraft authentication.
    PolygonMC is protected under GNU GENERAL PUBLIC LICENSE version 3.0 License
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
    https://github.com/DcmanProductions/PolygonMC
*/

namespace PolygonMC;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new MainPage();
        ConsoleWindow consoleWindow = new ConsoleWindow();
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        Window window = base.CreateWindow(activationState);

        window.Title = ApplicationName;

        window.MinimumWidth = 1280;
        window.MinimumHeight = 720;

        return window;
    }
}