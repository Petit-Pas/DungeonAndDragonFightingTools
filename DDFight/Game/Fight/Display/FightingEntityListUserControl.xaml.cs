using DDFight.Tools;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BaseToolsLibrary.DependencyInjection;
using DnDToolsLibrary.Fight;
using WpfToolsLibrary.Extensions;

namespace DDFight.Controlers.Fight
{
    /// <summary>
    /// Interaction logic for FightingCharacterListUserControl.xaml
    /// </summary>
    public partial class FightingEntityListUserControl : UserControl, IEventUnregisterable
    {
        private static readonly Lazy<IFightManager> _lazyFightManager = new(DIContainer.GetImplementation<IFightManager>());
        protected static IFightManager _fightManager => _lazyFightManager.Value;

        public FightingEntityListUserControl()
        {
            InitializeComponent();
            Loaded += FightingCharacterListUserControl_Loaded;
            FightersControl.LayoutUpdated += FightersControl_LayoutUpdated;
            _fightManager.PropertyChanged += FightersList_PropertyChanged;
        }

        private void FightersList_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.UnregisterAllChildren();
        }

        private void FightersControl_LayoutUpdated(object sender, EventArgs e)
        {
            if (FightersControl.Items.Count != 0 && GlobalContext.Context.FightContext.TurnIndex == 0 && GlobalContext.Context.FightContext.RoundCount == 0)
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
            FightersControl.ItemsSource = _fightManager.GetObservableCollection();
        }

        public void UnregisterToAll()
        {
        }
    }
}
