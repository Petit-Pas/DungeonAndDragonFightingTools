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

namespace DDFight.Game.Aggression.Spells.Display
{
    /// <summary>
    /// Logique d'interaction pour SpellListSpellBookUserControl.xaml
    /// </summary>
    public partial class SpellListSpellBookUserControl : UserControl
    {
        private SpellsList data_context
        {
            get => (SpellsList)DataContext;
        }

        public SpellListSpellBookUserControl()
        {
            InitializeComponent();

            Loaded += SpellListSpellBookUserControl_Loaded;
            AllSpellsFilterTextBoxControl.GotFocus += AllSpellsFilterTextBoxControl_GotFocus;
            AllSpellsFilterTextBoxControl.LostFocus += AllSpellsFilterTextBoxControl_LostFocus;
            AllSpellsFilterTextBoxControl.Text = filterPlaceholder;
            EntitySpellsFilterTextBoxControl.GotFocus += EntitySpellsFilterTextBoxControl_GotFocus;
            EntitySpellsFilterTextBoxControl.LostFocus += EntitySpellsFilterTextBoxControl_LostFocus;
            EntitySpellsFilterTextBoxControl.Text = filterPlaceholder;
        }

        #region Filtering

        private string filterPlaceholder = "Filter...";

        private void AllSpellsFilterTextBoxControl_GotFocus(object sender, RoutedEventArgs e)
        {
            if (AllSpellsFilterTextBoxControl.Text == filterPlaceholder)
                AllSpellsFilterTextBoxControl.Text = "";
        }

        private void EntitySpellsFilterTextBoxControl_GotFocus(object sender, RoutedEventArgs e)
        {
            if (EntitySpellsFilterTextBoxControl.Text == filterPlaceholder)
                EntitySpellsFilterTextBoxControl.Text = "";
        }

        private void AllSpellsFilterTextBoxControl_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AllSpellsFilterTextBoxControl.Text))
                AllSpellsFilterTextBoxControl.Text = filterPlaceholder;
        }

        private void EntitySpellsFilterTextBoxControl_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntitySpellsFilterTextBoxControl.Text))
                EntitySpellsFilterTextBoxControl.Text = filterPlaceholder;
        }
        private void SpellListSpellBookUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            AllSpellsControl.ItemsSource = Global.Context.SpellList.Spells;
            EntitySpellsControl.ItemsSource = data_context.Spells;
        }

        private void EntitySpellsFilterTextBoxControl_KeyUp(object sender, KeyEventArgs e)
        {
            EntitySpellsControl.FilterSpellListBox(EntitySpellsFilterTextBoxControl.Text);
        }

        private void AllSpellsFilterTextBoxControl_KeyUp(object sender, KeyEventArgs e)
        {
            AllSpellsControl.FilterSpellListBox(AllSpellsFilterTextBoxControl.Text);
        }

        #endregion Filtering

        #region EditLists

        private void addSpell(Spell spell)
        {
            data_context.AddSpell(spell);
        }

        private void AllSpellsControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                if (AllSpellsControl.SelectedIndex != -1)
                    addSpell((Spell)((Spell)AllSpellsControl.SelectedItem).Clone());
            }
        }

        private void removeSelectedEntitySpell()
        {
            data_context.RemoveSpell((Spell)EntitySpellsControl.SelectedItem);
        }

        private void EntitySpellsControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (EntitySpellsControl.SelectedIndex != -1)
                    removeSelectedEntitySpell();
            }
        }

        private void DeleteEntitySpellContextMenu_Click(object sender, RoutedEventArgs e)
        {
            if (EntitySpellsControl.SelectedIndex != -1)
            removeSelectedEntitySpell();
        }

        private void DuplicateEntitySpellContextMenu_Click(object sender, RoutedEventArgs e)
        {
            if (EntitySpellsControl.SelectedIndex != -1)
            {
                Spell new_spell = (Spell)((Spell)AllSpellsControl.SelectedItem).Clone();
                new_spell.Name += " - copy";
                addSpell(new_spell);
            }
        }

        private void editSpell(Spell spell)
        {
            spell.OpenEditWindow();
            data_context.Save();
        }

        private void EditEntitySpellContextMenu_Click(object sender, RoutedEventArgs e)
        {
            if (EntitySpellsControl.SelectedIndex != -1)
                editSpell((Spell)EntitySpellsControl.SelectedItem);
        }

        private void EntitySpellsControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (EntitySpellsControl.SelectedIndex != -1)
                editSpell((Spell)EntitySpellsControl.SelectedItem);
        }

        #endregion EditLists

        private void AddNewSpellButton_Click(object sender, RoutedEventArgs e)
        {
            Spell new_spell = new Spell();

            if (new_spell.OpenEditWindow())
                addSpell(new_spell);
        }
    }
}
