using System;
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

        static private bool already_loaded = false;

        private void SpellListSpellBookUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!already_loaded)
            {
                AllSpellsControl.DataContext = Global.Context.SpellList;
                EntitySpellsControl.DataContext = data_context;

                AllSpellsControl.EntityListControl.PreviewKeyDown += AllSpellList_PreviewKeyDown;
                MenuItem learn = new MenuItem { Header = "Learn this spell" };
                learn.Click += Learn_Click;
                AllSpellsControl.ListContextMenu.Items.Add(new Separator());
                AllSpellsControl.ListContextMenu.Items.Add(learn);

                AllSpellsControl.ListContextMenu = AllSpellsControl.ListContextMenu;
                already_loaded = true;
            }
        }

        private void Learn_Click(object sender, RoutedEventArgs e)
        {
            if (AllSpellsControl.EntityListControl.SelectedIndex != -1)
                data_context.AddSpell(AllSpellsControl.EntityListControl.SelectedItem as Spell);
        }

        private void AllSpellList_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (AllSpellsControl.EntityListControl.SelectedIndex != -1)
                    data_context.AddSpell((Spell)((Spell)AllSpellsControl.EntityListControl.SelectedItem).Clone());
                e.Handled = true;
            }
        }
    }
}
