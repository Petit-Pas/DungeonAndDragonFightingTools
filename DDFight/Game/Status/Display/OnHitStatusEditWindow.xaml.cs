using DDFight.Tools;
using DDFight.Windows;
using System.Windows;
using System.Windows.Controls;

namespace DDFight.Game.Status.Display
{
    /// <summary>
    /// Logique d'interaction pour EditCustomVerboseStatusWindow.xaml
    /// </summary>
    public partial class OnHitStatusEditWindow : Window
    {
        public OnHitStatus data_context
        {
            get => (OnHitStatus)DataContext;
        } 

        public OnHitStatusEditWindow()
        {
            InitializeComponent();
            
        }

        private void StringBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.AreAllChildrenValid())
                ValidateButtonControl.IsEnabled = true;
            else
                ValidateButtonControl.IsEnabled = false;
        }

        private bool planned_close = false;

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.AreAllChildrenValid())
            {
                data_context.Validated = true;
                planned_close = true;
                this.Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (planned_close == false)
            {
                AskYesNoWindow window = new AskYesNoWindow();
                AskYesNoDataContext dc = new AskYesNoDataContext { Message = "Are you sure you want to discard all your changes?" };

                window.DataContext = dc;
                window.ShowCentered();
                window.Owner = this;

                if (dc.Yes == false)
                    e.Cancel = true;
            }
        }
    }
}
