using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfToolsLibrary.Extensions
{
    public static class WindowExtensions
    {
        public static void ShowCentered(this Window window)
        {
            window.Owner = Application.Current.GetCurrentWindow();
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
    }
}
