using DnDToolsLibrary.Memory;
using DnDToolsLibrary.Status;
using System.Windows;

namespace DDFight.Game.Status.Display
{
    /// <summary>
    /// Interaction logic for EditCustomVerboseStatusListWindow.xaml
    /// </summary>
    //TODO unsure this file / functionality is still useful
    public partial class CustomVerboseStatusListEditWindow : Window
    {
        public GenericList<CustomVerboseStatus> data_context
        {
            get => (GenericList<CustomVerboseStatus>)DataContext;
        }

        public CustomVerboseStatusListEditWindow()
        {
            DataContextChanged += CustomVerboseStatusListEditWindow_DataContextChanged;
            InitializeComponent();
        }

        private void CustomVerboseStatusListEditWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            StatusListControl.EntityList = data_context;
            //data_context.Validated = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //data_context.Validated = true;
            //this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            /*if (data_context.Validated == false)
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
            }*/
        }
    }
}
