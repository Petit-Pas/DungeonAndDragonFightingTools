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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void EditableOnHitStatusList_Loaded(object sender, RoutedEventArgs e)
        {
            StatusListControl.ItemsSource = data_context.List;
        }

        private void AddStatusButton_Click(object sender, RoutedEventArgs e)
        {
            OnHitStatus _new = new OnHitStatus();

            Console.WriteLine("COCHON 123");
            if (_new.OpenEditWindow() == true)
            {
                data_context.List.Add(_new);
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
                    data_context.List.RemoveAt(StatusListControl.SelectedIndex);
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
