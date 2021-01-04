using DDFight.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DDFight.Controlers
{
    /// <summary>
    /// Logique d'interaction pour PrepareFightCharacterList.xaml
    /// </summary>
    public partial class SoonToFightEntityListUserControl : UserControl
    {
        private GameDataContext data_context {
            get => (GameDataContext)DataContext;
        }

        public SoonToFightEntityListUserControl()
        {
            InitializeComponent();

            Loaded += PrepareFightCharacterList_Loaded;

        }

        private void PrepareFightCharacterList_Loaded(object sender, RoutedEventArgs e)
        {
            CharactersList.ItemsSource = data_context.FightContext.FightersList.Fighters;
        }

        #region Add

        /// <summary>
        ///     add a new character
        /// </summary>
        private void add_fighter(PlayableEntity fighter)
        {
            data_context.FightContext.FightersList.Fighters.Add(fighter);
        }

        private void DuplicateFighter_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            PlayableEntity fighter = (PlayableEntity)CharactersList.SelectedItem;
            if (fighter is Monster)
            {
                IEnumerable<PlayableEntity> list = data_context.FightContext.FightersList.Fighters.Where(x => x.Name == ((Monster)CharactersList.SelectedItem).Name);
                PlayableEntity new_fighter = (PlayableEntity)(((Monster)CharactersList.SelectedItem).Clone());

                int i = 0;
                for (; i < list.Count(); i++)
                {
                    string tmp = new_fighter.Name + " - " + i;
                    if (list.ElementAt(i).DisplayName != tmp)
                        break;
                }
                new_fighter.DisplayName = new_fighter.Name + " - " + i;
                add_fighter(new_fighter);
            }
        }

        #endregion

        #region Delete

        private void delete_fighter(PlayableEntity entity)
        {
            data_context.FightContext.FightersList.Fighters.Remove(entity);
        }

        private void DeleteFighter_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            delete_fighter((PlayableEntity)CharactersList.SelectedItem);
        }

        #endregion

        private void CharactersList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
                this.DeleteFighter_MenuItem_Click(sender, null);
            if (e.Key == Key.Left)
                delete_fighter((PlayableEntity)CharactersList.SelectedItem);
        }

        private void FilterTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            CharactersList.FilterINameableListBox(FilterTextBox.TextBoxControl.Text);
        }
    }
}
