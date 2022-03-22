using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Dice.DiceCommancs.SavingThrowCommands.SavingThrowQueries;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WpfCustomControlLibrary.ModalWindows;
using WpfToolsLibrary.Extensions;
using WpfToolsLibrary.Navigation;

namespace WpfDnDCommandHandlers.DiceQueries.SavingThrowQueries
{
    /// <summary>
    /// Logique d'interaction pour SavingThrowQueryHandlerWindow.xaml
    /// </summary>
    public partial class SavingThrowQueryHandlerWindow : Window, IResultWindow<SavingThrowQuery, SavingThrow>
    {
        public SavingThrowQueryHandlerWindow()
        {
            InitializeComponent();
        }

        public bool Validated { get; set; } = false;

        private SavingThrowQuery data_context { get => DataContext as SavingThrowQuery; }

        public SavingThrow GetResult()
        {
            return data_context.SavingThrow;
        }

        public void LoadContext(SavingThrowQuery context)
        {
            DataContext = context;
        }

        private void Window_Error(object sender, ValidationErrorEventArgs e)
        {
            refresh_button();
        }

        private void Window_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            refresh_button();
        }

        private void Window_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            refresh_button();
        }

        private void refresh_button()
        {
            ValidateButton.IsEnabled = false;
            if (data_context.SavingThrow.SavingRoll != 0)
            {
                ValidateButton.IsEnabled = true;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.IsRollControlPressed(e))
            {
                e.Handled = true;
                if (!this.AreAllRollableChildrenRolled())
                    this.RollRollableChildren();
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (Validated == false)
            {
                YesNoWindow win = new YesNoWindow() { Text = "Are you sure you wish to cancel this ?", Validated = false };
                win.ShowCentered();

                if (!win.Validated)
                {
                    e.Cancel = true;
                }
            }
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            this.Validated = true;
            this.Close();
        }
    }
}
