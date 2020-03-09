using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DDFight.Game.Status.Display
{
    /// <summary>
    /// Logique d'interaction pour EditableCustomVerboseStatusList.xaml
    /// </summary>
    public partial class CustomVerboseStatusListEditableUserControl : UserControl
    {
        private PlayableEntity data_context
        {
            get => (PlayableEntity)DataContext;
        }

        public CustomVerboseStatusListEditableUserControl()
        {
            InitializeComponent();

            Loaded += EditableCustomVerboseStatusList_Loaded;
        }

        private void EditableCustomVerboseStatusList_Loaded(object sender, RoutedEventArgs e)
        {
            StatusListControl.ItemsSource = data_context.CustomVerboseStatusList.List;
        }

        private void AddStatusButton_Click(object sender, RoutedEventArgs e)
        {
            CustomVerboseStatus _new = new CustomVerboseStatus();

            if (_new.OpenEditWindow() == true)
            {
                data_context.CustomVerboseStatusList.List.Add(_new);
            }
        }

        // Keyboard Hanlder
        private void StatusListControl_KeyDown(object sender, KeyEventArgs e)
        {
            // Deletes Status
            if (e.Key == Key.Delete)
            {
                if (StatusListControl.SelectedIndex != -1)
                {
                    data_context.CustomVerboseStatusList.List.RemoveAt(StatusListControl.SelectedIndex);
                }
            }
        }

        /// <summary>
        ///     Edit Status
        /// </summary>
        private void StatusListControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (StatusListControl.SelectedIndex != -1)
            {
                ((CustomVerboseStatus)StatusListControl.SelectedItem).OpenEditWindow();
            }
        }
    }
}
