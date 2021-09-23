using DnDToolsLibrary.Entities;
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
            CharacterListControl.DataContext = GlobalContext.Context.CharacterList;
            MonsterListControl.DataContext = GlobalContext.Context.MonsterList;

            FighterListControl.ItemsSource = GlobalContext.Context.FightContext.FightersList;
        }

        private void FighterListControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.Delete || e.Key == Key.Back)
            {
                if (FighterListControl.SelectedIndex >= 0)
                {
                    GlobalContext.Context.FightContext.FightersList.RemoveElement(FighterListControl.SelectedItem as PlayableEntity);
                }
            }
        }
    }
}
