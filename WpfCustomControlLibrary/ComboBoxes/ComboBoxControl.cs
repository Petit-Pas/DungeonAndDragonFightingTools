using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using WpfToolsLibrary.Extensions;

namespace WpfCustomControlLibrary.ComboBoxes
{
    public class ComboBoxControl : ComboBox
    {
    
        private static readonly ResourceDictionary styleDict = new ResourceDictionary()
        {
            Source = new Uri("/WpfCustomControlLibrary;component/ComboBoxes/ComboBoxStyles.xaml", UriKind.RelativeOrAbsolute)
        };
        private static readonly Style defaultStyle = styleDict["ComboBoxStyle"] as Style;
        private static readonly Style itemStyle = styleDict["ComboBoxItemStyle"] as Style;

        public ComboBoxControl() : base()
        {
            if (this.Style == null && defaultStyle != null)
                this.Style = defaultStyle;
            if (this.ItemContainerStyle == null && itemStyle != null)
                this.ItemContainerStyle = itemStyle;
            this.FocusVisualStyle = null;
            this.KeyDown += ComboBoxControl_KeyDown;
            this.DropDownClosed += ComboBoxControl_DropDownClosed;
            this.MouseEnter += ComboBoxControl_MouseEnter;
            this.MouseLeave += ComboBoxControl_MouseLeave;
            this.DropDownClosed += ComboBoxControl_DropDownClosed1;
            this.GotFocus += ComboBoxControl_GotFocus;
            this.LostFocus += ComboBoxControl_LostFocus;
        }

        private void ComboBoxControl_LostFocus(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("LostFocus");
        }

        private void ComboBoxControl_GotFocus(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("GotFocus");
        }

        private void ComboBoxControl_DropDownClosed1(object sender, EventArgs e)
        {
            Console.WriteLine("Closed");
        }

        private void ComboBoxControl_MouseLeave(object sender, MouseEventArgs e)
        {
            Console.WriteLine("MouseLeave");

        }

        private void ComboBoxControl_MouseEnter(object sender, MouseEventArgs e)
        {
            Console.WriteLine("MouseEnter");
        }

        private void ComboBoxControl_DropDownClosed(object sender, EventArgs e)
        {
            /*ToggleButton btn =  this.Template.FindName("ToggleButton", this) as ToggleButton;
            Border border = btn.Template.FindName("Border", btn) as Border;
            ;
            border.Focus();*/
            Application.Current.GetCurrentWindow().Focus();
            this.Focus();
        }

        private void ComboBoxControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || e.Key == Key.Enter)
                this.IsDropDownOpen = true;
        }
    }
}
