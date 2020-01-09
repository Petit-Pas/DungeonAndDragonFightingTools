using DDFight.Game;
using DDFight.Game.Aggression.Attacks;
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
            if (data_context.HitAttacks != null)
            {
                this.AttacksList.Items.Clear();
                foreach (HitAttackTemplate atk in data_context.HitAttacks)
                {
                    this.AttacksList.Items.Add(atk);
                }
            }
        }

        #region Add

        /// <summary>
        ///     add a new character
        /// </summary>
        private void add_attack(HitAttackTemplate atk)
        {
            this.AttacksList.Items.Add(atk);
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
            AttacksList.Items.Remove(atk);
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
                this.AttacksList.Items[index] = temporary;
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
    }
}
