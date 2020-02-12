using DDFight.Windows;
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

namespace DDFight.Game.Status.Display
{
    /// <summary>
    /// Interaction logic for EditCustomVerboseStatusListWindow.xaml
    /// </summary>
    public partial class EditCustomVerboseStatusListWindow : Window
    {
        public PlayableEntity data_context
        {
            get => (PlayableEntity)DataContext;
        }

        public EditCustomVerboseStatusListWindow()
        {
            InitializeComponent();
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
                window.ShowDialog();

                if (dc.Yes == false)
                    e.Cancel = true;
            }
        }
    }
}
