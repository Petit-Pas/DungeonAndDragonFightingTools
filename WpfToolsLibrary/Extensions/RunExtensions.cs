using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace WpfToolsLibrary.Extensions
{
    public static class RunExtensions
    {
        public static Run BuildRun(string text, Brush color, int fontSize, FontWeight fontWeight)
        {
            Run result = new Run();

            result.Text = text;
            result.Foreground = color;
            result.FontSize = fontSize;
            result.FontWeight = fontWeight;

            return result;
        }

    }
}
