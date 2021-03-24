using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfCustomControlLibrary.InputControls.FilterTextBox
{
    public partial class FilterTextBoxControl : TextBox
    {
        private static readonly ResourceDictionary styleDict = new ResourceDictionary()
        {
            Source = new Uri("/WpfCustomControlLibrary;component/InputControls/FilterTextBox/FilterTextBoxStyle.xaml", UriKind.RelativeOrAbsolute)
        };
        private static readonly Style style = styleDict["FilterTextBoxStyle"] as Style;

        public FilterTextBoxControl() : base()
        {
            if (style != null)
                this.Style = style;
            DataContext = this;
            this.GotFocus += FilterTextBox_GotFocus; ;
            this.LostFocus += FilterTextBox_LostFocus; ;
            this.Text = filterPlaceHolder;
        }

        private readonly string filterPlaceHolder = "Filter...";

        private void FilterTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.Text))
            {
                this.Text = filterPlaceHolder;
            }
        }

        private void FilterTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.Text == filterPlaceHolder)
                this.Text = "";
        }

        public Brush BackgroundColor
        {
            get { return (Brush)this.GetValue(BackgroundColorProperty); }
            set { this.SetValue(BackgroundColorProperty, value); }
        }
        private static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.Register(
                nameof(BackgroundColor),
                typeof(Brush),
                typeof(FilterTextBoxControl),
                new PropertyMetadata(System.Windows.Application.Current.Resources["Gray"])
            );

        public Brush BorderColor
        {
            get { return (Brush)this.GetValue(BorderColorProperty); }
            set { this.SetValue(BorderColorProperty, value); }
        }
        private static readonly DependencyProperty BorderColorProperty = DependencyProperty.Register(
                nameof(BorderColor),
                typeof(Brush),
                typeof(FilterTextBoxControl),
                new PropertyMetadata(System.Windows.Application.Current.Resources["Light"])
            );

        public Brush ForegroundColor
        {
            get { return (Brush)this.GetValue(ForegroundColorProperty); }
            set { this.SetValue(ForegroundColorProperty, value); }
        }
        private static readonly DependencyProperty ForegroundColorProperty = DependencyProperty.Register(
                nameof(ForegroundColor),
                typeof(Brush),
                typeof(FilterTextBoxControl),
                new PropertyMetadata(System.Windows.Application.Current.Resources["Light"])
            );

        public Brush AccentColor
        {
            get { return (Brush)this.GetValue(AccentColorProperty); }
            set { this.SetValue(AccentColorProperty, value); }
        }
        public static readonly DependencyProperty AccentColorProperty = DependencyProperty.Register(
                nameof(AccentColor),
                typeof(Brush),
                typeof(FilterTextBoxControl),
                new PropertyMetadata(System.Windows.Application.Current.Resources["Indigo"])
            );

        public Brush LightAccentColor
        {
            get { return (Brush)this.GetValue(SelectionColorProperty); }
            set { this.SetValue(SelectionColorProperty, value); }
        }
        private static readonly DependencyProperty SelectionColorProperty = DependencyProperty.Register(
                nameof(LightAccentColor),
                typeof(Brush),
                typeof(FilterTextBoxControl),
                new PropertyMetadata(System.Windows.Application.Current.Resources["LightIndigo"])
            );
    }
}
