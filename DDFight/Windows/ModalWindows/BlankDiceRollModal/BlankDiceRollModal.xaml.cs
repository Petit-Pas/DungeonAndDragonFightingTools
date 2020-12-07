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

namespace DDFight.Windows.ModalWindows.BlankDiceRollModal
{
    /// <summary>
    /// Logique d'interaction pour BlankDiceRollModal.xaml
    /// </summary>
    public partial class BlankDiceRollModal : Window
    {
        private BlankDiceRollModalDataContext data_context
        {
            get => (BlankDiceRollModalDataContext)DataContext;
        }

        public BlankDiceRollModal()
        {
            InitializeComponent();
            LayoutUpdated += BlankDiceRollModal_LayoutUpdated;
        }

        private void BlankDiceRollModal_LayoutUpdated(object sender, EventArgs e)
        {
            ValidateButtonControl.IsEnabled = true;

            if (!(DiceRollControl.IsValid()))
            {
                ValidateButtonControl.IsEnabled = false;
            }                
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            if (DiceRollControl.IsValid())
            {
                data_context.Validated = true;
                this.Close();
            }
        }
    }
}
