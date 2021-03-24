using System;
using System.Windows;
using System.Windows.Media;

namespace WpfCustomControlLibrary.CardControls
{
    public class DownCardControl : BaseCardControl
    {
        private static readonly ResourceDictionary styleDict = new ResourceDictionary()
        {
            Source = new Uri("/WpfCustomControlLibrary;component/CardControls/DownCardStyle.xaml", UriKind.RelativeOrAbsolute)
        };
        private static readonly Style style = styleDict["DownCardStyle"] as Style;

        public DownCardControl()
            : base()
        {
            if (style != null)
                this.Style = style;
        }

        public Brush EnvironmentColor
        {
            get { return (Brush)this.GetValue(EnvironmentColorProperty); }
            set { this.SetValue(EnvironmentColorProperty, value); }
        }
        private static readonly DependencyProperty EnvironmentColorProperty = DependencyProperty.Register(
                nameof(EnvironmentColor),
                typeof(Brush),
                typeof(DownCardControl),
                new PropertyMetadata(System.Windows.Application.Current.Resources["LightestGray"])
            );
    }
}
