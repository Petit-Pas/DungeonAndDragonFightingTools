using DDFight.Controlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Game.Aggression.Spells.Display
{
    public class SpellListEditableUserControl : BaseListEditableUserControl
    {
        public SpellListEditableUserControl() : base()
        {
            DataContextChanged += SpellListEditableUserControl_DataContextChanged;
            OnInitialized(null);
        }

        private SpellsList data_context
        {
            get
            {
                try { return DataContext as SpellsList; }
                catch (Exception) { return null; }
            }
        }

        private void refresh_entityList()
        {
            EntityList = data_context?.Spells;
        }


        private void SpellListEditableUserControl_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            refresh_entityList();
        }

        #region ListControl

        public override void edit(object obj)
        {
            Spell spell = obj as Spell;

            if (spell.Edit())
                data_context.Save();
        }

        public override void remove(object obj)
        {
            Spell spell = obj as Spell;
            data_context.RemoveSpell(spell);
        }

        public override void duplicate(object obj)
        {
            Spell spell = obj as Spell;
            Spell new_spell = spell.Clone() as Spell;
            new_spell.Name = new_spell.Name + " - Copy";
            add_new(new_spell);
        }

        public override void add_new(object obj = null)
        {
            Spell spell = obj as Spell;
            if (spell == null)
                spell = new Spell();
            if (spell.Edit())
                data_context.AddSpell(spell);
        }

        #endregion ListControl

    }
}
