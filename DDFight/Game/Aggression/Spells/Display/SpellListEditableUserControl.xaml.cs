using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DDFight.Game.Aggression.Spells.Display
{
    /// <summary>
    /// Interaction logic for SpellListEditableUserControl.xaml
    /// </summary>
    public partial class SpellListEditableUserControl : UserControl
    {
        /// <summary>
        ///     Getter for a casted DataContext
        /// </summary>
        private SpellsList data_context
        {
            get => (SpellsList)DataContext;
        }

        /// <summary>
        ///     Ctor
        /// </summary>
        public SpellListEditableUserControl()
        {
            Loaded += SpellListUserControl_Loaded;
            InitializeComponent();
            FilterTextBox.GotFocus += FilterTextBox_GotFocus;
            FilterTextBox.LostFocus += FilterTextBox_LostFocus;
            FilterTextBox.Text = filterPlaceHolder;
        }

        private string filterPlaceHolder = "Filter...";

        private void FilterTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FilterTextBox.Text))
                FilterTextBox.Text = filterPlaceHolder;
        }

        private void FilterTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (FilterTextBox.Text == filterPlaceHolder)
                FilterTextBox.Text = "";
        }

        /// <summary>
        ///     Initializer for when the DataContext is available
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpellListUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SpellList.ItemsSource = data_context.Spells;
        }

        #region Add

        /// <summary>
        ///     add a new spell
        /// </summary>
        private void add_spell(Spell spell)
        {
            data_context.AddSpell(spell);
        }

        /// <summary>
        ///     handler for the Add character button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddSpell_Button_Click(object sender, RoutedEventArgs e)
        {
            Spell spell = new Spell();

            if (spell.OpenEditWindow())
                add_spell(spell);
        }

        private void DuplicateSpell_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Spell spell = (Spell)SpellList.SelectedItem;
            Spell new_spell = (Spell)spell.Clone();

            new_spell.Name = new_spell.Name + " - Copie";

            add_spell(new_spell);
        }

        #endregion

        #region Delete

        private void delete_spell(Spell spell)
        {
            data_context.RemoveSpell(spell);
        }

        /// <summary>
        ///     handler for the Remove spell button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveSpell_Button_Click(object sender, RoutedEventArgs e)
        {
            delete_spell((Spell)SpellList.SelectedItem);
        }

        private void DeleteSpell_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            delete_spell((Spell)SpellList.SelectedItem);
        }

        #endregion

        #region Edit

        private void update_spell(Spell to_update)
        {
            to_update.OpenEditWindow();
            Global.Context.SpellList.Save();
        }

        /// <summary>
        ///     Handler for the double click on a spell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpellList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SpellList.SelectedItem != null)
            {
                update_spell((Spell)SpellList.SelectedItem);
            }
        }

        /// <summary>
        ///     Hendler for Edit selection on menu Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditSpell_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            update_spell((Spell)SpellList.SelectedItem);
        }

        #endregion

        private void SpellList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
                this.DeleteSpell_MenuItem_Click(sender, null);
        }

        private void FilterTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            SpellList.FilterINameableListBox(FilterTextBox.Text);
        }
    }
}
