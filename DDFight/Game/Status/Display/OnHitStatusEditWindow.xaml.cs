using DDFight.Windows;
using DnDToolsLibrary.Status;
using System.Windows;
using WpfToolsLibrary.Extensions;

namespace DDFight.Game.Status.Display
{
    /// <summary>
    /// Logique d'interaction pour EditCustomVerboseStatusWindow.xaml
    /// </summary>
    public partial class OnHitStatusEditWindow : Window
    {
        public bool Validated = false;

        public OnHitStatus data_context
        {
            get => (OnHitStatus)DataContext;
        } 

        public OnHitStatusEditWindow()
        {
            DataContextChanged += OnHitStatusEditWindow_DataContextChanged;
            InitializeComponent();
        }

        private void OnHitStatusEditWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            data_context.PropertyChanged += Data_context_PropertyChanged;
        }

        private void Data_context_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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
                this.Validated = true;
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
