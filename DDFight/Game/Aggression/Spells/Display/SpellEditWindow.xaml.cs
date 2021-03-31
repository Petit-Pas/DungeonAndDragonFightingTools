using DDFight.Resources;
using DDFight.Windows;
using DnDToolsLibrary.Attacks.Spells;
using System.Windows;
using WpfToolsLibrary.Extensions;

namespace DDFight.Game.Aggression.Spells.Display
{
    /// <summary>
    /// Interaction logic for SpellEditWindow.xaml
    /// </summary>
    public partial class SpellEditWindow : Window
    {
        public Spell data_context 
        {
            get => (Spell)DataContext;
        }

        private bool self_close = false;

        public SpellEditWindow()
        {
            InitializeComponent();
        }

        public bool Validated = false;

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!this.AreAllChildrenValid())
            {
                StatusMessageWindowDataContext context = new StatusMessageWindowDataContext
                {
                    Message = "At least one of the parameters is wrong",
                    Icon = ResourceManager.BmUnchecked(),
                };
                StatusMessageWindow message = new StatusMessageWindow
                {
                    Title = "Cannot validate",
                    DataContext = context,
                    Owner = this,
                };
                message.ShowCentered();
            }
            else
            {
                data_context.Validated = true;
                self_close = true;
                Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            data_context.Validated = false;
            self_close = true;
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!self_close)
            {
                // user exited the window manually
                AskYesNoDataContext context = new AskYesNoDataContext
                {
                    Message = "Your changes will be discarded, do you want to proceed ?",
                };
                AskYesNoWindow window = new AskYesNoWindow
                {
                    Owner = this,
                    DataContext = context,
                };
                window.ShowCentered();

                if (context.Yes)
                {
                    data_context.Validated = false;
                }
                else 
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
