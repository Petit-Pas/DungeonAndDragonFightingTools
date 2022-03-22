using System;
using System.Windows;
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

        public static void AnimateDouble(FrameworkElement element, DependencyProperty property, double from, double to, TimeSpan duration)
        {
            DoubleAnimation double_animation = new DoubleAnimation(from, to, duration);
            element.BeginAnimation(property, double_animation);
        }

        public static void AnimateDouble(FrameworkElement element, DependencyProperty property, double to, TimeSpan duration)
        {
            DoubleAnimation double_animation = new DoubleAnimation(to, duration);
            element.BeginAnimation(property, double_animation);
        }
    }
}
