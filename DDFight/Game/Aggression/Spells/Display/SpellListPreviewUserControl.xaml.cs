using System.Windows.Controls;
using System.Windows.Input;

namespace DDFight.Game.Aggression.Spells.Display
{
    /// <summary>
    /// Interaction logic for SpellListPreviewUserControl.xaml
    /// </summary>
    public partial class SpellListPreviewUserControl : UserControl
    {
        private PlayableEntity data_context
        {
            get => (PlayableEntity)DataContext;
        }

        public SpellListPreviewUserControl()
        {
            InitializeComponent();
        }

        private void SpellListControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SpellListControl.SelectedIndex != -1)
                SpellPreviewControl.DataContext = SpellListControl.SelectedItem;
            else
                SpellPreviewControl.DataContext = null;
        }

        private void SpellListControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SpellListControl.SelectedIndex != -1) 
            {
                Spell spell = (Spell)SpellListControl.SelectedItem;
                spell.CastSpell(data_context);
            }
        }
    }
}
