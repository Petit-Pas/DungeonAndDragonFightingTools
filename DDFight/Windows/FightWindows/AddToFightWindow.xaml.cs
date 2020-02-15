using DDFight.Game;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            CharacterListControl.ItemsSource = Global.Context.CharacterList.Characters;
            MonsterListControl.ItemsSource = Global.Context.MonsterList.Monsters;

            FighterListControl.ItemsSource = Global.Context.FightContext.FightersList.Fighters;
        }

        private void CharacterListControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                if (CharacterListControl.SelectedItem != null)
                {
                    ((CharacterDataContext)CharacterListControl.SelectedItem).DisplayName = ((CharacterDataContext)CharacterListControl.SelectedItem).Name;
                    Global.Context.FightContext.FightersList.AddCharacter((CharacterDataContext)CharacterListControl.SelectedItem);
                }
            }
        }

        private void MonsterListControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                if (MonsterListControl.SelectedItem != null)
                {
                    ((MonsterDataContext)MonsterListControl.SelectedItem).DisplayName = ((MonsterDataContext)MonsterListControl.SelectedItem).Name;
                    Global.Context.FightContext.FightersList.AddMonster((MonsterDataContext)MonsterListControl.SelectedItem);
                }
            }
        }
    }
}
