using BaseToolsLibrary.IO;
using System;

namespace WpfToolsLibrary.ConsoleTools
{
    public class WpfFontColorProvider : FontColorProvider
    {
        private IFontColor defaultValue = null;

        public override IFontColor GetDefault()
        {
            if (defaultValue == null)
                throw new InvalidOperationException ("Default Value was not set in WpfFontColorProvider");
            return defaultValue;
        }

        public override void SetDefault(IFontColor color)
        {
            defaultValue = color;
        }
    }
}
