using DDFight.Tools;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DDFight.Controlers.Fight
{
    /// <summary>
    /// Interaction logic for FightingCharacterListUserControl.xaml
    /// </summary>
    public partial class FightingCharacterListUserControl : UserControl, IEventUnregisterable
    {
        public FightingCharacterListUserControl()
        {
            InitializeComponent();
            Loaded += FightingCharacterListUserControl_Loaded;
            FightersControl.LayoutUpdated += FightersControl_LayoutUpdated;
        }


        private void FightersControl_LayoutUpdated(object sender, EventArgs e)
        {
            if (FightersControl.Items.Count != 0 && Global.Context.FightContext.TurnIndex == 0 && Global.Context.FightContext.RoundCount == 0)
            {
                ContentPresenter uiElement = (ContentPresenter)FightersControl.ItemContainerGenerator.ContainerFromIndex(0);
                GroupBox gb = (GroupBox)uiElement.GetFirstChildByName("CharacterTileGroupBoxControl");
                gb.Background = (Brush)Application.Current.Resources["Indigo"];
            }
        }

        private void FightingCharacterListUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FightersControl.ItemsSource = Global.Context.FightContext.FightersList.Fighters;
        }

        public void UnregisterToAll()
        {
            FightersControl.LayoutUpdated -= FightersControl_LayoutUpdated;
        }
    }
}
