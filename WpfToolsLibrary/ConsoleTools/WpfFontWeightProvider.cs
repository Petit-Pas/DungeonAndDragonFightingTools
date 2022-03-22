using BaseToolsLibrary.IO;
using System.Windows;

namespace WpfToolsLibrary.ConsoleTools
{
    public class WpfFontWeightProvider : IFontWeightProvider
    {
        public IFontWeight Bold => new WpfFontWeight() { FontWeight = FontWeights.Bold };

        public IFontWeight SemiBold => new WpfFontWeight() { FontWeight = FontWeights.SemiBold };

        public IFontWeight Normal => new WpfFontWeight() { FontWeight = FontWeights.Normal };
    }
}
