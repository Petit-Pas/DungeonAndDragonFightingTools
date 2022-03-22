using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfCustomControlLibrary.ShadowBoxes
{
    public class BaseSimpleShadowBoxControl : ContentControl
    {
        private static readonly ResourceDictionary styleDict = new ResourceDictionary()
        {
            Source = new Uri("/WpfCustomControlLibrary;component/ShadowBoxes/BaseSimpleShadowBoxStyle.xaml", UriKind.RelativeOrAbsolute)
        };
        private static readonly Style style = styleDict["BaseSimpleShadowBoxStyle"] as Style;

        public BaseSimpleShadowBoxControl() : base()
        {
            if (this.Style == null && style != null)
                this.Style = style;
        }

        public Brush BackgroundColor
        {
            get { return (Brush)this.GetValue(BackgroundColorProperty); }
            set { this.SetValue(BackgroundColorProperty, value); }
        }
        private static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.Register(
            nameof(BackgroundColor),
            typeof(Brush),
            typeof(BaseSimpleShadowBoxControl),
            new PropertyMetadata(System.Windows.Application.Current.Resources["LightestGray"]));

        public Color ShadowColor
        {
            get { return (Color)this.GetValue(ShadowColorProperty); }
            set { this.SetValue(ShadowColorProperty, value); }
        }
        private static readonly DependencyProperty ShadowColorProperty = DependencyProperty.Register(
            nameof(ShadowColor),
            typeof(Color),
            typeof(BaseSimpleShadowBoxControl),
            new PropertyMetadata(Colors.Black));
    }
}
