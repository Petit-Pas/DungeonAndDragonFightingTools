using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DDFight.Controlers.InputBoxes
{
    /// <summary>
    /// Logique d'interaction pour FilterTextBoxControl.xaml
    /// </summary>
    public partial class FilterTextBoxControl : UserControl
    {
        public FilterTextBoxControl()
        {
            InitializeComponent();
            TextBoxControl.GotFocus += FilterTextBox_GotFocus; ;
            TextBoxControl.LostFocus += FilterTextBox_LostFocus; ;
            TextBoxControl.Text = filterPlaceHolder;
        }

        private string filterPlaceHolder = "Filter...";

        private void FilterTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxControl.Text))
                TextBoxControl.Text = filterPlaceHolder;
        }

        private void FilterTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxControl.Text == filterPlaceHolder)
                TextBoxControl.Text = "";
        }
    }
}
