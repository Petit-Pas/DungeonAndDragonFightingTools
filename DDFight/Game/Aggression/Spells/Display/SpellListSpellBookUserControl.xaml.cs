using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        }

        private void SpellListSpellBookUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            AllSpellsControl.DataContext = Global.Context.SpellList;
            EntitySpellsControl.DataContext = data_context;
            AllSpellsControl.SpellList.PreviewKeyDown += AllSpellList_PreviewKeyDown;
        }

        private void AllSpellList_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (AllSpellsControl.SpellList.SelectedIndex != -1)
                    addSpell((Spell)((Spell)AllSpellsControl.SpellList.SelectedItem).Clone());
                e.Handled = true;
            }
        }

        #region EditLists

        private void addSpell(Spell spell)
        {
            data_context.AddSpell(spell);
        }

        private void removeSelectedEntitySpell()
        {
            data_context.RemoveSpell((Spell)EntitySpellsControl.SpellList.SelectedItem);
        }

        private void EntitySpellsControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (EntitySpellsControl.SpellList.SelectedIndex != -1)
                    removeSelectedEntitySpell();
            }
        }

        private void DeleteEntitySpellContextMenu_Click(object sender, RoutedEventArgs e)
        {
            if (EntitySpellsControl.SpellList.SelectedIndex != -1)
            removeSelectedEntitySpell();
        }

        private void DuplicateEntitySpellContextMenu_Click(object sender, RoutedEventArgs e)
        {
            if (EntitySpellsControl.SpellList.SelectedIndex != -1)
            {
                Spell new_spell = (Spell)((Spell)AllSpellsControl.SpellList.SelectedItem).Clone();
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
            if (EntitySpellsControl.SpellList.SelectedIndex != -1)
                editSpell((Spell)EntitySpellsControl.SpellList.SelectedItem);
        }

        private void EntitySpellsControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (EntitySpellsControl.SpellList.SelectedIndex != -1)
                editSpell((Spell)EntitySpellsControl.SpellList.SelectedItem);
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
