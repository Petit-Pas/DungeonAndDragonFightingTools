using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.GetInputSpellLevel;
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

namespace WpfDnDCommandHandlers.AttackCommands.SpellCommands.GetInputSpellLevel.GetInputCantripLevel
{ 
    /// <summary>
    /// Logique d'interaction pour CantripLevelSelectorWindow.xaml
    /// </summary>
    public partial class GetInputCantripLevelWindow : Window, IResultWindow<GetInputCantripLevelCommand, GetInputCantripLevelResponse>
    {
        public GetInputCantripLevelWindow() 
        {
            Initialized += NormalSpellLevelSelectorWindow_Initialized;
            InitializeComponent();
        }

        private void NormalSpellLevelSelectorWindow_Initialized(object sender, EventArgs e)
        {
            CircularSelector.SelectedLevelChanged += CircularSelector_SelectedLevelChanged;
        }

        private void CircularSelector_SelectedLevelChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void refresh()
        {
            ValidateButton.IsEnabled = false;
            if (CircularSelector.SelectedLevel >= 1 && CircularSelector.SelectedLevel <= 9)
            {
                ValidateButton.IsEnabled = true;
            }
        }

        public bool Validated { get; set; } = false;

        public int SelectedLevel { get; set; }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedLevel = CircularSelector.SelectedLevel;
            this.Validated = true;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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

        public GetInputCantripLevelResponse GetResult()
        {
            return new GetInputCantripLevelResponse(SelectedLevel);
        }

        public void LoadContext(GetInputCantripLevelCommand context)
        {
        }
    }
}
