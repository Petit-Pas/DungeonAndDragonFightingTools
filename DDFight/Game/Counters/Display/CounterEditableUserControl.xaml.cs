using DDFight.ValidationRules;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
