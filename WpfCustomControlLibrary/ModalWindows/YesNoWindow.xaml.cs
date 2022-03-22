using System.Windows;
using System.Windows.Input;

namespace WpfCustomControlLibrary.ModalWindows
{
    /// <summary>
    /// Interaction logic for YesNoWindow.xaml
    /// </summary>
    public partial class YesNoWindow : Window
    {
        public YesNoWindow()
        {
            InitializeComponent();
        }

        public string Text { get; set ; }

        public bool Validated = false;

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            Validated = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Y)
                ValidateButton_Click(this, null);
            else if (e.Key == Key.Escape || e.Key == Key.N)
                CancelButton_Click(this, null);
        }
    }
}
