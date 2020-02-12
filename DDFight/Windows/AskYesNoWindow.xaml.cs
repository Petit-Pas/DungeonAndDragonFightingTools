using System.Windows;
using System.Windows.Input;

namespace DDFight.Windows
{
    /// <summary>
    ///     Logique d'interaction pour AskYesNoWindow.xaml
    /// </summary>
    public partial class AskYesNoWindow : Window
    {
        private AskYesNoDataContext data_context
        {
            get => (AskYesNoDataContext)DataContext;
        }

        public AskYesNoWindow()
        {
            InitializeComponent();
        }

        private void ClickYes(object sender, RoutedEventArgs e)
        {
            data_context.Yes = true;
            this.Close();
        }

        private void ClickNo(object sender, RoutedEventArgs e)
        {
            data_context.Yes = false;
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Y)
            {
                data_context.Yes = true;
                this.Close();
            }
            else if (e.Key == Key.Back || e.Key == Key.Escape || e.Key == Key.N)
            {
                data_context.Yes = false;
                this.Close();
            }
        }
    }
}
