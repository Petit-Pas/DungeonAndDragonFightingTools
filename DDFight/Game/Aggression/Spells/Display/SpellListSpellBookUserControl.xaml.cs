using DDFight.Tools.Save;
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
        private SpellList data_context
        {
            get => (SpellList)DataContext;
        }

        public SpellListSpellBookUserControl()
        {
            Initialized += SpellListSpellBookUserControl_Initialized;
            DataContextChanged += SpellListSpellBookUserControl_DataContextChanged;
            InitializeComponent();

        }

        private void SpellListSpellBookUserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            EntitySpellsControl.DataContext = data_context;
        }

        private void SpellListSpellBookUserControl_Initialized(object sender, EventArgs e)
        {
            AllSpellsControl.DataContext = Global.Context.SpellList;

            AllSpellsControl.EntityListControl.PreviewKeyDown += AllSpellList_PreviewKeyDown;
            MenuItem learn = new MenuItem { Header = "Learn this spell" };
            learn.Click += Learn_Click;
            AllSpellsControl.ListContextMenu.Items.Add(new Separator());
            AllSpellsControl.ListContextMenu.Items.Add(learn);

            AllSpellsControl.ListContextMenu = AllSpellsControl.ListContextMenu;
        }

        private void Learn_Click(object sender, RoutedEventArgs e)
        {
            if (AllSpellsControl.EntityListControl.SelectedIndex != -1)
                data_context.AddElement(AllSpellsControl.EntityListControl.SelectedItem as Spell);
        }

        private void AllSpellList_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (AllSpellsControl.EntityListControl.SelectedIndex != -1)
                    data_context.AddElementSilent((Spell)((Spell)AllSpellsControl.EntityListControl.SelectedItem).Clone());
                e.Handled = true;
            }
        }
    }
}
