using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WpfCustomControlLibrary.Animations.Extensions
{
    public static class BorderAnimationExtension
    {
        public static void AnimateBackground(this Border border, Color from, Color to, TimeSpan duration)
        {
            ColorAnimation color_animation = new ColorAnimation(from, to, duration);
            SolidColorBrush brush = new SolidColorBrush();
            border.Background = brush;
            brush.BeginAnimation(SolidColorBrush.ColorProperty, color_animation);
        }
    }
}
