using DDFight.Tools;
using System.Windows;
using System.Windows.Input;

namespace DDFight.Windows.ModalWindows.FormWindow
{
    /// <summary>
    /// Interaction logic for AskPositiveIntWindow.xaml
    /// </summary>
    public partial class AskPositiveIntWindow : Window
    {

        public int Number
        {
            get => _number;
            set
            {
                _number = value;
            }
        }
        private int _number = 0;

        public AskPositiveIntWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public bool Validated = false;

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.AreAllChildrenValid())
            {
                Validated = true;
                this.Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Validated == false)
            {
                AskYesNoDataContext ctx = new AskYesNoDataContext() { 
                    Message = "Are you sure you want to cancel this spell?",
                };
                AskYesNoWindow win = new AskYesNoWindow() { DataContext = ctx };
                win.ShowCentered();

                if (ctx.Yes)
                {
                    Validated = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void PositiveIntTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            ValidateButtonControl.IsEnabled = false;
            if (this.AreAllChildrenValid())
                ValidateButtonControl.IsEnabled = true;
        }
    }
}
