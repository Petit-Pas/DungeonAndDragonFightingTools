using DDFight.Game;
using DDFight.Game.Aggression.Attacks;
using DDFight.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DDFight.Controlers.Game.Attacks
{
    /// <summary>
    /// Interaction logic for EditableListHitAttackTemplate.xaml
    /// </summary>
    public partial class EditableListHitAttackTemplate : UserControl
    {
        private PlayableEntity data_context
        {
            get => (PlayableEntity)DataContext;
        }
        public EditableListHitAttackTemplate()
        {
            InitializeComponent();

            Loaded += EditableListHitAttackTemplate_Loaded;
        }

        private void EditableListHitAttackTemplate_Loaded(object sender, RoutedEventArgs e)
        {
            AttacksList.ItemsSource = data_context.HitAttacks;
        }

        #region Add

        /// <summary>
        ///     add a new character
        /// </summary>
        private void add_attack(HitAttackTemplate atk)
        {
            data_context.HitAttacks.Add(atk);

        }

        /// <summary>
        ///     handler for the Add character button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddAttack_Button_Click(object sender, RoutedEventArgs e)
        {
            HitAttackTemplate atk = new HitAttackTemplate();

            add_attack(atk);
        }

        private void DuplicateAttack_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            HitAttackTemplate atk = (HitAttackTemplate)AttacksList.SelectedItem;
            HitAttackTemplate new_atk = (HitAttackTemplate)atk.Clone();

            new_atk.Name = new_atk.Name + " - Copie";

            add_attack(new_atk);
        }

        #endregion

        #region Delete

        private void delete_attack(HitAttackTemplate atk)
        {
            data_context.HitAttacks.Remove(atk);
        }

        /// <summary>
        ///     handler for the Remove character button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveAttack_Button_Click(object sender, RoutedEventArgs e)
        {
            delete_attack((HitAttackTemplate)AttacksList.SelectedItem);
        }

        private void DeleteAttack_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            delete_attack((HitAttackTemplate)AttacksList.SelectedItem);
        }

        #endregion

        #region Edit

        private void update_attack(HitAttackTemplate to_update)
        {
            EditHitAttackTemplateWindow window = new EditHitAttackTemplateWindow
            {
                Owner = Window.GetWindow(this),
            };
            HitAttackTemplate temporary = (HitAttackTemplate)to_update.Clone();
            window.DataContext = temporary;

            window.ShowDialog();

            if (temporary.Validated == true)
            {
                int index = this.AttacksList.SelectedIndex;
                data_context.HitAttacks[index] = temporary;
            }
        }

        /// <summary>
        ///     Handler for the double click on a character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AttackList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (AttacksList.SelectedItem != null)
            {
                update_attack((HitAttackTemplate)AttacksList.SelectedItem);
            }
        }

        /// <summary>
        ///     Hendler for Edit selection on menu Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditAttack_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            update_attack((HitAttackTemplate)AttacksList.SelectedItem);
        }

        #endregion

        private void AttacksList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
                this.DeleteAttack_MenuItem_Click(sender, null);
        }
    }
}
