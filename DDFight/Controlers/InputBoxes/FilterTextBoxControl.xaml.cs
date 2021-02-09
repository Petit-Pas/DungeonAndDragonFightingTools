using System.Windows;
using System.Windows.Controls;

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

        private readonly string filterPlaceHolder = "Filter...";

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
