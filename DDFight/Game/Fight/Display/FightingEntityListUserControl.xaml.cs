using DDFight.Tools;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DDFight.Controlers.Fight
{
    /// <summary>
    /// Interaction logic for FightingCharacterListUserControl.xaml
    /// </summary>
    public partial class FightingEntityListUserControl : UserControl, IEventUnregisterable
    {
        public FightingEntityListUserControl()
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
                gb.BorderThickness = new Thickness(2);
                FightersControl.LayoutUpdated -= FightersControl_LayoutUpdated;
            }
        }

        private void FightingCharacterListUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FightersControl.ItemsSource = Global.Context.FightContext.FightersList.Fighters;
        }

        public void UnregisterToAll()
        {
        }
    }
}
