using DDFight.Windows;
using System.Windows;

namespace DDFight.Game.Status.Display
{
    /// <summary>
    /// Interaction logic for EditCustomVerboseStatusListWindow.xaml
    /// </summary>
    public partial class CustomVerboseStatusListEditWindow : Window
    {
        public CustomVerboseStatusList data_context
        {
            get => (CustomVerboseStatusList)DataContext;
        }

        public CustomVerboseStatusListEditWindow()
        {
            InitializeComponent();
            DataContextChanged += CustomVerboseStatusListEditWindow_DataContextChanged;
        }

        private void CustomVerboseStatusListEditWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            data_context.Validated = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            data_context.Validated = true;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (data_context.Validated == false)
            {
                AskYesNoWindow window = new AskYesNoWindow();
                AskYesNoDataContext dc = new AskYesNoDataContext
                {
                    Message = "Are you sure you want to Discard your changes ?",
                };

                window.DataContext = dc;
                window.ShowCentered();

                if (dc.Yes == false)
                    e.Cancel = true;
            }
        }
    }
}
