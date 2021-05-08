using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WpfCustomControlLibrary.Animations
{
    public delegate void BrushSetter(SolidColorBrush brush);

    public static class AnimationLibrary
    {
        public static void AnimateBrush(BrushSetter brushSetter, Color from, Color to, TimeSpan duration)
        {
            ColorAnimation color_animation = new ColorAnimation(from, to, duration);
            SolidColorBrush brush = new SolidColorBrush();
            brushSetter(brush);
            brush.BeginAnimation(SolidColorBrush.ColorProperty, color_animation);
        }
        public static void AnimateBrush(BrushSetter brushSetter, Color to, TimeSpan duration)
        {
            ColorAnimation color_animation = new ColorAnimation(to, duration);
            SolidColorBrush brush = new SolidColorBrush();
            brushSetter(brush);
            brush.BeginAnimation(SolidColorBrush.ColorProperty, color_animation);
        }
    }
}
