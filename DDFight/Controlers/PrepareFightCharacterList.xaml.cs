using DDFight.Game;
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

namespace DDFight.Controlers
{
    /// <summary>
    /// Logique d'interaction pour PrepareFightCharacterList.xaml
    /// </summary>
    public partial class PrepareFightCharacterList : UserControl
    {
        private GameDataContext data_context {
            get => (GameDataContext)DataContext;
        }

        public PrepareFightCharacterList()
        {
            InitializeComponent();

            Loaded += PrepareFightCharacterList_Loaded;

        }

        private void PrepareFightCharacterList_Loaded(object sender, RoutedEventArgs e)
        {
            CharactersList.ItemsSource = data_context.FightingCharacters;
        }

        #region Add

        /// <summary>
        ///     add a new character
        /// </summary>
        private void add_fighter(PlayableEntity fighter)
        {
            data_context.FightingCharacters.Add(fighter);

        }

        private void DuplicateFighter_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            PlayableEntity fighter = (PlayableEntity)CharactersList.SelectedItem;
            if (fighter is MonsterDataContext)
            {
                IEnumerable<PlayableEntity> list = data_context.FightingCharacters.Where(x => x.Name == ((MonsterDataContext)CharactersList.SelectedItem).Name);
                PlayableEntity new_fighter = (PlayableEntity)(((MonsterDataContext)CharactersList.SelectedItem).Clone());

                new_fighter.DisplayName = new_fighter.Name + " - " + list.Count().ToString();
                add_fighter(new_fighter);
            }
        }

        #endregion

        #region Delete

        private void delete_fighter(PlayableEntity entity)
        {
            data_context.FightingCharacters.Remove(entity);
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
    }
}
