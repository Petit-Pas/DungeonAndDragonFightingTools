using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.NonAttackSpellResultsQueries;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.NonAttackSpellResultQueries;
using WpfCustomControlLibrary.ModalWindows;
using WpfToolsLibrary.Extensions;
using WpfToolsLibrary.Navigation;

namespace WpfDnDCommandHandlers.AttackQueries.SpellQueries.NonAttackSpellResultsQueries
{
    /// <summary>
    /// Logique d'interaction pour GetInputSpellNonAttackResultsWindow.xaml
    /// </summary>
    public partial class NonAttackSpellResultsQueryHandlerWindow : Window, IResultWindow<NonAttackSpellResultsQuery, NonAttackSpellResults>
    {
        public NonAttackSpellResultsQueryHandlerWindow()
        {
            InitializeComponent();
        }

        private NonAttackSpellResultsQuery data_context { get => DataContext as NonAttackSpellResultsQuery; }

        public bool Validated { get; set; } = false;

        public NonAttackSpellResults GetResult()
        {
            return data_context.SpellResults;
        }

        public void LoadContext(NonAttackSpellResultsQuery context)
        {
            DataContext = context;
            refresh_button();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Validated == false)
            {
                YesNoWindow win = new YesNoWindow() { Text = "Are you sure you wish to cancel this spell?", Validated = false };
                win.ShowCentered();

                if (!win.Validated)
                {
                    e.Cancel = true;
                }
            }
        }

        private void CastButton_Click(object sender, RoutedEventArgs e)
        {
            this.Validated = true;
            this.Close();
        }

        private void refresh_button()
        {
            CastButton.IsEnabled = false;
            if (this.AreAllChildrenValid() && this.AreAllRollableChildrenRolled())
                CastButton.IsEnabled = true;
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
    }
}
