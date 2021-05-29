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
        }

        private void ComboBoxControl_DropDownClosed(object sender, EventArgs e)
        {
            ToggleButton btn =  this.Template.FindName("ToggleButton", this) as ToggleButton;
            Border border = btn.Template.FindName("Border", btn) as Border;
            ;
            border.Focus();
        }

        private void ComboBoxControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || e.Key == Key.Enter)
                this.IsDropDownOpen = true;
        }
    }
}
