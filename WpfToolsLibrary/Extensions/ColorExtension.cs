using System.Windows.Media;

namespace WpfToolsLibrary.Extensions
{
    public static class ColorExtension
    {
        public static string ToARGB(this Color color)
        {
            return $"{color.A}{color.R}{color.G}{color.B}";
        }
    }
}
