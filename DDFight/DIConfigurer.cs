using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using WpfToolsLibrary.ConsoleTools;

namespace DDFight
{
    public static class DIConfigurer
    {
        public static void Configure()
        {
            // REGISTER

            // Console
            DIContainer.RegisterSingleton<ICustomConsole, WpfConsole>();
            DIContainer.RegisterSingleton<IFontColorProvider, WpfFontColorProvider>();
            DIContainer.RegisterSingleton<IFontWeightProvider, WpfFontWeightProvider>();


            // VERIFY
            DIContainer.Verify();

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

    }
}
