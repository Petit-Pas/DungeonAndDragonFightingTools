using BaseToolsLibrary.IO;
using System;
using System.Windows.Media;

namespace WpfToolsLibrary.ConsoleTools
{
    public class WpfFontColorProvider : FontColorProvider
    {
        public WpfFontColorProvider()
        {
            Success = new WpfFontColor() {Brush = new SolidColorBrush(Colors.Green)};
            Failure = new WpfFontColor() {Brush = new SolidColorBrush(Colors.Red)};
        }

        private IFontColor defaultValue = null;

        public override IFontColor GetDefault()
        {
            if (defaultValue == null)
                throw new InvalidOperationException ("Default Value was not set in WpfFontColorProvider");
            return defaultValue;
        }

        public override IFontColor Success { get; }
        public override IFontColor Failure { get; }

        public override void SetDefault(IFontColor color)
        {
            defaultValue = color;
        }
    }
}
