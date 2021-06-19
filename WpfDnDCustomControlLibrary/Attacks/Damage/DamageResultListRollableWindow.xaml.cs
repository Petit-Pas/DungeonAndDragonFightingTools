using DnDToolsLibrary.Attacks.AttacksCommands.DamageCommands.CalculateDamageResultList;
using DnDToolsLibrary.Attacks.Damage;
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
using WpfCustomControlLibrary.ModalWindows;
using WpfToolsLibrary.Extensions;
using WpfToolsLibrary.Navigation;

namespace WpfDnDCustomControlLibrary.Attacks.Damage
{
    /// <summary>
    /// Logique d'interaction pour DamageResultListRollableWindow.xaml
    /// </summary>
    public partial class DamageResultListRollableWindow : Window, IValidableWindow
    {
        private CalculateDamageResultListCommand data_context
        {
            get => DataContext as CalculateDamageResultListCommand;
        }
        public bool Validated { get; set; }

        public DamageResultListRollableWindow()
        {
            DataContextChanged += DamageResultListRollableWindow_DataContextChanged;
            InitializeComponent();
            ValidateButton.IsEnabled = false;
        }

        private void DamageResultListRollableWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (data_context != null)
            {
                foreach (DamageResult dmg in data_context.DamageList.Elements)
                {
                    dmg.PropertyChanged += Dmg_PropertyChanged;
                }
            }
        }

        private void Dmg_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ValidateButton.IsEnabled = false;
            if (this.AreAllRollableChildrenRolled())
                ValidateButton.IsEnabled = true;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.IsRollControlPressed(e))
            {
                e.Handled = true;
                this.RollRollableChildren();
            }
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            Validated = true;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Validated == false || this.AreAllRollableChildrenRolled() == false)
            {
                YesNoWindow win = new YesNoWindow() { Text = "Are you sure you wish to cancel this ?", Validated = false };
                win.ShowCentered();

                if (win.Validated)
                {
                    e.Cancel = false;
                }
            }
        }
    }
}
