using DDFight.Game.Entities;
using System;
using System.Windows;
using System.Windows.Input;

namespace DDFight.Windows.FightWindows
{
    /// <summary>
    /// Logique d'interaction pour AddToFightWindow.xaml
    /// </summary>
    public partial class AddToFightWindow : Window
    {
        public AddToFightWindow()
        {
            InitializeComponent();

            Loaded += AddToFightWindow_Loaded;
        }

        private void AddToFightWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CharacterListControl.DataContext = Global.Context.CharacterList;
            MonsterListControl.DataContext = Global.Context.MonsterList;

            FighterListControl.ItemsSource = Global.Context.FightContext.FightersList.Elements;
        }

        private void FighterListControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.Delete || e.Key == Key.Back)
            {
                if (FighterListControl.SelectedIndex >= 0)
                {
                    Global.Context.FightContext.FightersList.RemoveElement(FighterListControl.SelectedItem as PlayableEntity);
                }
            }
        }
    }
}
