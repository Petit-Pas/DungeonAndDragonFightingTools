using DDFight.Controlers;
using DDFight.WpfExtensions;
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

        public override bool edit(object element)
        {
            if (element is Spell spell)
                return spell.OpenEditWindow();
            return false;
        }

        private void refresh_entityList()
        {
            EntityList = data_context;
        }

        private void SpellListEditableUserControl_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            refresh_entityList();
        }
    }
}
