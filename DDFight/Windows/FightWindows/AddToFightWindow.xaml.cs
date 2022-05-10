using System;
using DnDToolsLibrary.Entities;
using System.Windows;
using System.Windows.Input;
using BaseToolsLibrary.DependencyInjection;
using DnDToolsLibrary.Fight;

namespace DDFight.Windows.FightWindows
{
    /// <summary>
    /// Logique d'interaction pour AddToFightWindow.xaml
    /// </summary>
    public partial class AddToFightWindow : Window
    {
        private static readonly Lazy<IFightManager> _lazyFightManager = new (DIContainer.GetImplementation<IFightManager>());
        protected static IFightManager _fightManager => _lazyFightManager.Value;

        public AddToFightWindow()
        {
            InitializeComponent();

            Loaded += AddToFightWindow_Loaded;
        }

        private void AddToFightWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CharacterListControl.DataContext = GlobalContext.Context.CharacterList;
            MonsterListControl.DataContext = GlobalContext.Context.MonsterList;

            FighterListControl.ItemsSource = _fightManager.GetObservableCollection();
        }

        private void FighterListControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.Delete || e.Key == Key.Back)
            {
                if (FighterListControl.SelectedIndex >= 0)
                {
                    _fightManager.RemoveFighter(FighterListControl.SelectedItem as PlayableEntity);
                }
            }
        }
    }
}
