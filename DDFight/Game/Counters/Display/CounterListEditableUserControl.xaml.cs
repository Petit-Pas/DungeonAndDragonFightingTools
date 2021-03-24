using DDFight.Tools;
using DDFight.ValidationRules;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfToolsLibrary.ValidationRules;

namespace DDFight.Game.Counters.Display
{
    /// <summary>
    /// Logique d'interaction pour CounterListEditableUserControl.xaml
    /// </summary>
    public partial class CounterListEditableUserControl : UserControl, IValidable
    {
        private CounterList data_context 
        {
            get => (CounterList)DataContext;
        }

        public CounterListEditableUserControl()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            data_context.AddElementSilent(new Counter());
        }

        private void removeSelected()
        {
            if (CounterListControl.SelectedIndex != -1)
            {
                data_context.RemoveElement(CounterListControl.SelectedItem as Counter);
                RemoveButtonControl.IsEnabled = false;
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            removeSelected();
        }

        private void CounterListControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RemoveButtonControl.IsEnabled = true;
        }

        private void CounterListControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
                removeSelected();
        }

        public bool IsValid()
        {
            CounterListControl.AreAllChildrenValid();
            return this.AreAllChildrenValid();
        }
    }
}
