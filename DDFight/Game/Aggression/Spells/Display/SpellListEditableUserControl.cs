using DDFight.Controlers;
using DDFight.Tools.Save;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
