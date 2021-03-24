using System;
using System.Windows;
using System.Windows.Media;

namespace WpfCustomControlLibrary.CardControls
{
    public class UpCardControl : BaseCardControl
    {
        private static readonly ResourceDictionary styleDict = new ResourceDictionary()
        {
            Source = new Uri("/WpfCustomControlLibrary;component/CardControls/UpCardStyle.xaml", UriKind.RelativeOrAbsolute)
        };
        private static readonly Style style = styleDict["UpCardStyle"] as Style;

        public UpCardControl() 
            : base ()
        {
            if (style != null)
                this.Style = style;
        }

        public Brush BorderColor
        {
            get { return (Brush)this.GetValue(BorderColorProperty); }
            set { this.SetValue(BorderColorProperty, value); }
        }
        private static readonly DependencyProperty BorderColorProperty = DependencyProperty.Register(
                nameof(BorderColor),
                typeof(Brush),
                typeof(UpCardControl),
                new PropertyMetadata(System.Windows.Application.Current.Resources["LightestGray"])
            );
    }
}
