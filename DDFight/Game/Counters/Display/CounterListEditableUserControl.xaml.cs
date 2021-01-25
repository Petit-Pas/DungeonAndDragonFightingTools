using DDFight.ValidationRules;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DDFight.Game.Counters.Display
{
    /// <summary>
    /// Logique d'interaction pour CounterListEditableUserControl.xaml
    /// </summary>
    public partial class CounterListEditableUserControl : UserControl, IValidable
    {
        private ObservableCollection<Counter> data_context 
        {
            get => (ObservableCollection<Counter>)DataContext;
        }

        public CounterListEditableUserControl()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            data_context.Add(new Counter());
        }

        private void removeSelected()
        {
            if (CounterListControl.SelectedIndex != -1)
            {
                data_context.RemoveAt(CounterListControl.SelectedIndex);
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
