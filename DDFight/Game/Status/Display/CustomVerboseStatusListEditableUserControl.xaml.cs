using DDFight.ListExtensions;
using DDFight.WpfExtensions;
using DnDToolsLibrary.Status;
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
        private CustomVerboseStatusList data_context
        {
            get => (CustomVerboseStatusList)DataContext;
        }

        public CustomVerboseStatusListEditableUserControl()
        {
            InitializeComponent();

            Loaded += EditableCustomVerboseStatusList_Loaded;
        }

        private void EditableCustomVerboseStatusList_Loaded(object sender, RoutedEventArgs e)
        {
            StatusListControl.ItemsSource = data_context.Elements;
        }

        private void AddStatusButton_Click(object sender, RoutedEventArgs e)
        {
            data_context.AddElement();
        }

        // Keyboard Hanlder
        private void StatusListControl_KeyDown(object sender, KeyEventArgs e)
        {
            // Deletes Status
            if (e.Key == Key.Delete)
            {
                if (StatusListControl.SelectedIndex != -1)
                {
                    data_context.RemoveElement((CustomVerboseStatus)StatusListControl.SelectedItem);
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
                if (StatusListControl.SelectedItem is CustomVerboseStatus status)
                    status.OpenEditWindow();
            }
        }
    }
}
