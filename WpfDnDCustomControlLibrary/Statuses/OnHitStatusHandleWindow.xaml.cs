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
using WpfCustomControlLibrary.ModalWindows;
using WpfToolsLibrary.Extensions;

namespace WpfDnDCustomControlLibrary.Statuses
{
    /// <summary>
    ///     This window is to be used either for Savings (be it application or resist later on) and for damage application.
    /// </summary>
    public partial class OnHitStatusHandleWindow : Window
    {
        public OnHitStatusHandleWindow()
        {
            InitializeComponent();
        }

        public bool Validated = false;
        private bool selfClosing = false;

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            selfClosing = true;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (selfClosing == false)
            {
                YesNoWindow window = new YesNoWindow { 
                    Title = "Are you sure?",
                    Text = "Are you sure you wish to cancel this?"
                };
                window.ShowCentered();
                if (window.Validated == false)
                    e.Cancel = true;
            }
        }
    }
}
