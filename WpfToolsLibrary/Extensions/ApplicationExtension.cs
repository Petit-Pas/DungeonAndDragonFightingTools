using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfToolsLibrary.Extensions
{
    public static class ApplicationExtension
    {

        private static Window lastFetched = null;
        public static Window GetCurrentWindow(this Application app)
        {
            if (lastFetched == null || !lastFetched.IsActive)
                lastFetched = app.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            return lastFetched;
        }
    }
}
