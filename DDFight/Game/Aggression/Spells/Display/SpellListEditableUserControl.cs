using DDFight.Controlers;
using DnDToolsLibrary.Attacks.Spells;

namespace DDFight.Game.Aggression.Spells.Display
{
    public class SpellListEditableUserControl : SpecializedListUserControl<Spell>
    {
        public SpellListEditableUserControl() : base()
        {
            DataContextChanged += SpellListEditableUserControl_DataContextChanged;
        }

        private SpellList data_context
        {
            get => DataContext as SpellList;
        }

        private void refresh_entityList()
        {
            EntityList = data_context?.Elements;
        }

        private void SpellListEditableUserControl_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            refresh_entityList();
        }
    }
}
