using DDFight.Game;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DDFight.Controlers
{
    /// <summary>
    /// Interaction logic for MonsterListUserControl.xaml
    /// </summary>
    public partial class MonsterListEditableUserControl : UserControl
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
        public MonsterListEditableUserControl()
        {
            Loaded += MonsterListUserControl_Loaded;

            InitializeComponent();
            FilterControl.TextBoxControl.KeyUp += FilterTextBox_KeyUp;
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
        private void add_Monster(Monster Monster)
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
            Monster Monster = new Monster();

            if (Monster.OpenEditWindow())
                add_Monster(Monster);
        }

        private void DuplicateMonster_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Monster Monster = (Monster)MonsterList.SelectedItem;
            Monster new_Monster = (Monster)Monster.Clone();

            new_Monster.Name = new_Monster.Name + " - Copie";

            add_Monster(new_Monster);
        }

        #endregion

        #region Delete

        private void delete_Monster(Monster Monster)
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
            delete_Monster((Monster)MonsterList.SelectedItem);
        }

        private void DeleteMonster_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            delete_Monster((Monster)MonsterList.SelectedItem);
        }

        #endregion

        #region Edit

        private void update_Monster(Monster to_update)
        {
            to_update.OpenEditWindow();
            Global.Context.MonsterList.Save();
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
                update_Monster((Monster)MonsterList.SelectedItem);
            }
        }

        /// <summary>
        ///     Hendler for Edit selection on menu Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditMonster_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            update_Monster((Monster)MonsterList.SelectedItem);
        }

        #endregion

        #region AddToFight

        private void AddToFight_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (MonsterList.SelectedItem == null)
                return;
            data_context.FightContext.FightersList.AddMonster((Monster)MonsterList.SelectedItem);
        }

        #endregion

        private void MonsterList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
                this.DeleteMonster_MenuItem_Click(sender, null);
            if (e.Key == Key.Right)
                AddToFight_MenuItem_Click(sender, null);
        }

        private void FilterTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            MonsterList.FilterINameableListBox(FilterControl.TextBoxControl.Text);
        }
    }
}
