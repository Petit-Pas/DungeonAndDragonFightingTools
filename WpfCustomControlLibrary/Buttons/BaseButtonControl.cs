using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfCustomControlLibrary.Buttons
{
    public class BaseButtonControl : Button
    {
        private static readonly ResourceDictionary styleDict = new ResourceDictionary()
        {
            Source = new Uri("/WpfCustomControlLibrary;component/Buttons/BaseButtonStyle.xaml", UriKind.RelativeOrAbsolute)
        };
        private static readonly Style style = styleDict["BaseButtonStyle"] as Style;

        public BaseButtonControl() : base()
        {
            if (style != null)
                this.Style = style;
        }

        public Brush BaseColor
        {
            get { return (Brush)this.GetValue(BaseColorProperty); }
            set { this.SetValue(BaseColorProperty, value); }
        }
        private static readonly DependencyProperty BaseColorProperty = DependencyProperty.Register(
            nameof(BaseColor),
            typeof(Brush),
            typeof(BaseButtonControl),
            new PropertyMetadata(System.Windows.Application.Current.Resources["Gray"]));

        public Brush HoverColor
        {
            get { return (Brush)this.GetValue(HoverColorProperty); }
            set { this.SetValue(HoverColorProperty, value); }
        }
        private static readonly DependencyProperty HoverColorProperty = DependencyProperty.Register(
            nameof(HoverColor),
            typeof(Brush),
            typeof(BaseButtonControl),
            new PropertyMetadata(System.Windows.Application.Current.Resources["Indigo"]));

        public Brush ClickColor
        {
            get { return (Brush)this.GetValue(ClickColorProperty); }
            set { this.SetValue(ClickColorProperty, value); }
        }
        private static readonly DependencyProperty ClickColorProperty = DependencyProperty.Register(
            nameof(ClickColor),
            typeof(Brush),
            typeof(BaseButtonControl),
            new PropertyMetadata(System.Windows.Application.Current.Resources["LightIndigo"]));

    }
}
