using DDFight.Windows;
using DnDToolsLibrary.Status;
using System.Windows;
using System.Windows.Controls;
using WpfToolsLibrary.Extensions;
using WpfToolsLibrary.Navigation;

namespace DDFight.Game.Status.Display
{
    /// <summary>
    /// Logique d'interaction pour EditCustomVerboseStatusWindow.xaml
    /// </summary>
    public partial class CustomVerboseStatusEditWindow : Window, IValidableWindow
    {
        public bool Validated { get; set; } = false;
        public CustomVerboseStatus data_context
        {
            get => (CustomVerboseStatus)DataContext;
        } 

        public CustomVerboseStatusEditWindow()
        {
            InitializeComponent();
            
            Loaded += EditCustomVerboseStatusWindow_Loaded;
        }


        private void EditCustomVerboseStatusWindow_Loaded(object sender, RoutedEventArgs e)
        {
            HeaderBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            HeaderBox.TextChanged += StringBox_TextChanged;
        }

        private void StringBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.AreAllChildrenValid())
                ErrorMessage.Visibility = Visibility.Collapsed;
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
            else
            {
                ErrorMessage.Visibility = Visibility.Visible;
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
