using System.Windows;

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
