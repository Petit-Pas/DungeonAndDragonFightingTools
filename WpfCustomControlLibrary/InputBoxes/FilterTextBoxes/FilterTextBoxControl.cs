using System.Windows;
using WpfCustomControlLibrary.InputBoxes.BaseTextBoxes;

namespace WpfCustomControlLibrary.InputBoxes.FilterTextBoxes
{
    public class FilterTextBoxControl : BaseTextBoxControl
    {
        public FilterTextBoxControl() : base()
        {
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
    }
}
