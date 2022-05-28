using System;
using System.Collections.Generic;

namespace BaseToolsLibrary.IO
{
    public abstract class FontColorProvider : IFontColorProvider
    {
        private Dictionary<string, IFontColor> colors = new Dictionary<string, IFontColor>();

        public void AddKey(string key, IFontColor color)
        {
            if (colors.ContainsKey(key))
                throw new InvalidOperationException($"The key '{key}' has already been registered to this FontColorProvider");
            colors.Add(key, color);
        }

        public IFontColor GetColorByKey(string key)
        {
            if (!colors.ContainsKey(key))
            {
                Logger.Log($"WARNING: Unknown Key '{key}' in {this.GetType().ToString()}.GetColorByKey, using default value");
                return GetDefault();
            }
            return colors[key];
        }

        public abstract void SetDefault(IFontColor color);

        public abstract IFontColor GetDefault();
        public abstract IFontColor Success { get; }
        public abstract IFontColor Failure { get; }
    }
}
