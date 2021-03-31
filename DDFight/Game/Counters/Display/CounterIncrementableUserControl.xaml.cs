using DnDToolsLibrary.Counters;
using System.Windows;
using System.Windows.Controls;

namespace DDFight.Game.Counters.Display
{
    /// <summary>
    /// Logique d'interaction pour CounterIncrementableUserControl.xaml
    /// </summary>
    public partial class CounterIncrementableUserControl : UserControl
    {
        private Counter data_context
        {
            get => (Counter)DataContext;
        }

        public CounterIncrementableUserControl()
        {
            InitializeComponent();
            DataContextChanged += CounterIncrementableUserControl_DataContextChanged;
        }

        private void refresh_buttons()
        {
            PlusButtonControl.IsEnabled = true;
            MinusButtonControl.IsEnabled = true;
            if (data_context.CurrentValue == 0)
                MinusButtonControl.IsEnabled = false;
            if (data_context.MaxValue != 0 && data_context.CurrentValue == data_context.MaxValue)
                PlusButtonControl.IsEnabled = false;
        }

        private void CounterIncrementableUserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            refresh_buttons();
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            data_context.CurrentValue -= 1;
            refresh_buttons();
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            data_context.CurrentValue += 1;
            refresh_buttons();
        }
    }
}
