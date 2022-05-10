using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Fight;
using System.Windows;
using System.Windows.Media;
using WpfToolsLibrary.ConsoleTools;

namespace DDFight
{
    public static class DIConfigurer
    {
        public static void ConfigureCore(bool mediator_param = false)
        {
            // REGISTER 
            // Mediator
            DIContainer.RegisterSingleton<IMediator, BaseMediator>(new BaseMediator(mediator_param));

            var _ = new FightersManager();

            DIContainer.RegisterSingleton<IFightManager, FightersManager>(_);
            DIContainer.RegisterSingleton<IStatusProvider, StatusProvider>();

            // CONFIGURE
        }

        public static void ConfigureWpf()
        {
            // REGISTER

            // Console
            DIContainer.RegisterSingleton<ICustomConsole, WpfConsole>();
            DIContainer.RegisterSingleton<IFontColorProvider, WpfFontColorProvider>();
            DIContainer.RegisterSingleton<IFontWeightProvider, WpfFontWeightProvider>();

            // CONFIGURE

            // WpfConsole
            ICustomConsole console = DIContainer.GetImplementation<ICustomConsole>();
            console.DefaultFontColor = new WpfFontColor() { Brush = Application.Current.Resources["Light"] as SolidColorBrush };
            // FontColorProvider
            IFontColorProvider colorProvider = DIContainer.GetImplementation<IFontColorProvider>();
            colorProvider.SetDefault(new WpfFontColor() { Brush = Application.Current.Resources["Light"] as SolidColorBrush });
            colorProvider.AddKey("Light", new WpfFontColor() { Brush = Application.Current.Resources["Light"] as SolidColorBrush });
            colorProvider.AddKey("Acid", new WpfFontColor() { Brush = new SolidColorBrush(Colors.YellowGreen) });
            colorProvider.AddKey("Cold", new WpfFontColor() { Brush = new SolidColorBrush(Colors.Aqua) });
            colorProvider.AddKey("Fire", new WpfFontColor() { Brush = new SolidColorBrush(Colors.Red) });
            colorProvider.AddKey("Lightning", new WpfFontColor() { Brush = new SolidColorBrush(Colors.Yellow) });
            colorProvider.AddKey("Necrotic", new WpfFontColor() { Brush = new SolidColorBrush(Colors.Black) });
            colorProvider.AddKey("Poison", new WpfFontColor() { Brush = new SolidColorBrush(Colors.Green) });
            colorProvider.AddKey("Psychic", new WpfFontColor() { Brush = new SolidColorBrush(Colors.Purple) });
            colorProvider.AddKey("Radiant", new WpfFontColor() { Brush = new SolidColorBrush(Colors.Orange) });
        }

        /// <summary>
        ///     DIContainer should not be updated after this call
        /// </summary>
        public static void Verify()
        {
            // VERIFY
            DIContainer.Verify();
        }
    }
}
