using DDFight.ListExtensions;
using DDFight.WpfExtensions;
using DnDToolsLibrary.Status;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DDFight.Game.Status.Display
{
    /// <summary>
    /// Logique d'interaction pour OnHitStatusListEditableUserControl.xaml
    /// </summary>
    public partial class OnHitStatusListEditableUserControl : UserControl
    {
        private OnHitStatusList data_context
        {
            get => (OnHitStatusList)DataContext;
        }

        public OnHitStatusListEditableUserControl()
        {
            InitializeComponent();

            Loaded += EditableOnHitStatusList_Loaded;
        }

        public bool HasSavingThrow
        {
            get { return (bool)GetValue(HasSavingThrowProperty); }
            set { SetValue(HasSavingThrowProperty, value); }
        }

        public static readonly DependencyProperty HasSavingThrowProperty =
            DependencyProperty.Register(nameof(HasSavingThrow), typeof(bool),
                typeof(OnHitStatusListEditableUserControl));
        private void EditableOnHitStatusList_Loaded(object sender, RoutedEventArgs e)
        {
            StatusListControl.ItemsSource = data_context;
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
                    data_context.RemoveElement(StatusListControl.SelectedItem as OnHitStatus);
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
                ((OnHitStatus)StatusListControl.SelectedItem).OpenEditWindow();
            }
        }
    }
}
