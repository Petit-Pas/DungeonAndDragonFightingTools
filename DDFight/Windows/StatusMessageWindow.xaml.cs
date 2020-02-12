using System.Windows;
using System.Windows.Input;

namespace DDFight.Windows
{
    /// <summary>
    /// Logique d'interaction pour StatusMessageWindow.xaml
    /// </summary>
    public partial class StatusMessageWindow : Window
    {
        /// <summary>
        ///     Ctor
        /// </summary>
        public StatusMessageWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Exits on button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        ///     Exits on Enter, Escape and Space key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Escape || e.Key == Key.Space)
            {
                this.Close();
            }
        }
    }
}
