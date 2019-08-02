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
using System.Windows.Shapes;

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
