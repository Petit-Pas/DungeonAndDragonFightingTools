using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfCustomControlLibrary.ListControls.ListBoxControls
{
    public class ListBoxControl : ListBox
    {
        private static readonly ResourceDictionary styleDict = new ResourceDictionary()
        {
            Source = new Uri("/WpfCustomControlLibrary;component/ListControls/ListBoxControls/ListBoxStyle.xaml", UriKind.RelativeOrAbsolute)
        };
        private static readonly Style style = styleDict["ListBoxStyle"] as Style;
        private static readonly Style itemStyle = styleDict["ListBoxItemStyle"] as Style;


        public ListBoxControl() : base()
        {
            if (style != null && this.Style == null)
                this.Style = style;
            if (itemStyle != null && this.ItemContainerStyle == null)
                this.ItemContainerStyle = itemStyle;
        }

        public int ScrollbarSmallChange
        {
            get { return (int)this.GetValue(ScrollbarSmallChangeProperty); }
            set { this.SetValue(ScrollbarSmallChangeProperty, value); }
        }
        private static readonly DependencyProperty ScrollbarSmallChangeProperty = DependencyProperty.Register(
            nameof(ScrollbarSmallChange),
            typeof(int),
            typeof(ListBoxControl),
            new PropertyMetadata(10));

        public int ScrollbarLargeChange
        {
            get { return (int)this.GetValue(ScrollbarLargeChangeProperty); }
            set { this.SetValue(ScrollbarLargeChangeProperty, value); }
        }
        private static readonly DependencyProperty ScrollbarLargeChangeProperty = DependencyProperty.Register(
            nameof(ScrollbarLargeChange),
            typeof(int),
            typeof(ListBoxControl),
            new PropertyMetadata(100));

    }
}
