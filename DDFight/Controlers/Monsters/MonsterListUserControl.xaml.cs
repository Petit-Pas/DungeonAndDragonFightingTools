using DDFight.Game;
using DDFight.Windows;
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
    /// Interaction logic for MonsterListUserControl.xaml
    /// </summary>
    public partial class MonsterListUserControl : UserControl
    {
        /// <summary>
        ///     Getter for a casted DataContext
        /// </summary>
        private GameDataContext data_context
        {
            get
            {
                return (GameDataContext)DataContext;
            }
        }

        /// <summary>
        ///     Ctor
        /// </summary>
        public MonsterListUserControl()
        {
            Loaded += MonsterListUserControl_Loaded;

            InitializeComponent();
        }

        /// <summary>
        ///     Initializer for when the DataContext is available
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonsterListUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MonsterList.ItemsSource = data_context.MonsterList.Monsters;
        }

        #region Add

        /// <summary>
        ///     add a new Monster
        /// </summary>
        private void add_Monster(MonsterDataContext Monster)
        {
            data_context.MonsterList.AddMonster(Monster);

        }

        /// <summary>
        ///     handler for the Add Monster button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddMonster_Button_Click(object sender, RoutedEventArgs e)
        {
            MonsterDataContext Monster = new MonsterDataContext();

            add_Monster(Monster);
        }

        private void DuplicateMonster_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MonsterDataContext Monster = (MonsterDataContext)MonsterList.SelectedItem;
            MonsterDataContext new_Monster = (MonsterDataContext)Monster.Clone();

            new_Monster.Name = new_Monster.Name + " - Copie";

            add_Monster(new_Monster);
        }

        #endregion

        #region Delete

        private void delete_Monster(MonsterDataContext Monster)
        {
            data_context.MonsterList.RemoveMonster(Monster);
        }

        /// <summary>
        ///     handler for the Remove Monster button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveMonster_Button_Click(object sender, RoutedEventArgs e)
        {
            delete_Monster((MonsterDataContext)MonsterList.SelectedItem);
        }

        private void DeleteMonster_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            delete_Monster((MonsterDataContext)MonsterList.SelectedItem);
        }

        #endregion

        #region Edit

        private void update_Monster(MonsterDataContext to_update)
        {
            EditMonsterWindow window = new EditMonsterWindow
            {
                Owner = Window.GetWindow(this),
            };
            MonsterDataContext temporary = (MonsterDataContext)to_update.Clone();
            window.DataContext = temporary;

            window.ShowDialog();

            if (temporary.Validated == true)
            {
                data_context.MonsterList.Replace(to_update, temporary);
                data_context.MonsterList.Save();
            }
        }

        /// <summary>
        ///     Handler for the double click on a Monster
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonsterList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MonsterList.SelectedItem != null)
            {
                update_Monster((MonsterDataContext)MonsterList.SelectedItem);
            }
        }

        /// <summary>
        ///     Hendler for Edit selection on menu Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditMonster_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            update_Monster((MonsterDataContext)MonsterList.SelectedItem);
        }

        #endregion

        #region AddToFight

        private void AddToFight_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<PlayableEntity> list = data_context.FightingCharacters.Where(x => x.Name == ((MonsterDataContext)MonsterList.SelectedItem).Name);
            PlayableEntity tmp = (PlayableEntity)(((MonsterDataContext)MonsterList.SelectedItem).Clone());
            
            tmp.DisplayName = tmp.Name + " - " + list.Count().ToString();
            data_context.FightingCharacters.Add(tmp);
        }

        #endregion

        private void MonsterList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
                this.DeleteMonster_MenuItem_Click(sender, null);
        }
    }
}
