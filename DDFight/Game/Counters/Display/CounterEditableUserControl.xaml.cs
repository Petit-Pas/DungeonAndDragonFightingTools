using DnDToolsLibrary.Counters;
using System.Windows.Controls;
using WpfToolsLibrary.ValidationRules;

namespace DDFight.Game.Counters.Display
{
    /// <summary>
    /// Logique d'interaction pour CounterEditableUserControl.xaml
    /// </summary>
    public partial class CounterEditableUserControl : UserControl, IValidable
    {
        private Counter data_context 
        {
            get => (Counter)DataContext;
        }

        public CounterEditableUserControl()
        {
            InitializeComponent();
        }

        public bool IsValid()
        {
            if (data_context.MaxValue != 0 && data_context.CurrentValue > data_context.MaxValue)
                return false;
            return true;
        }
    }
}
