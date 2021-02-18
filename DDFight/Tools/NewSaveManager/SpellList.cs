using DDFight.Game.Aggression.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Tools.Save
{
    public class SpellList : GenericList<Spell>
    {
        public SpellList()
        {
            IsMainSpellList = false;
            init_handlers();
        }

        public void init_handlers()
        {
            this.ListChanged += NewPlayableEntityList_ListChanged;
            this.ListElementChanged += NewPlayableList_ListElementChanged;

        }

        public SpellList(bool isMainSpellList = false)
        {
            this.IsMainSpellList = isMainSpellList;
            init_handlers();
        }

        public bool IsMainSpellList = false;

        private void NewPlayableList_ListElementChanged(object sender, ListElementChangedArgs e)
        {
            if (!Global.Loading)
                switch (e.Operation)
                {
                    case GenericListOperation.Addition:
                        if (IsMainSpellList)
                            SaveManager.SaveUnique<Spell>(e.Element);
                        break;
                    case GenericListOperation.Deletion:
                        if (IsMainSpellList)
                            SaveManager.DeleteUnique<Spell>(e.Element);
                        break;
                    case GenericListOperation.Modification:
                        if (IsMainSpellList)
                            SaveManager.SaveUnique<Spell>(e.Element);
                        break;
                    case GenericListOperation.Sort:
                        break;
                }
        }

        private void NewPlayableEntityList_ListChanged(object sender, ListChangedArgs e)
        {
            if (!Global.Loading)
            {
                switch (e.Operation)
                {
                    case GenericListOperation.Addition:
                        this.SortElements((x, y) => {
                            return x.Name.CompareTo(y.Name);
                        });
                        break;
                    case GenericListOperation.Deletion:
                        break;
                    case GenericListOperation.Modification:
                        break;
                    case GenericListOperation.Sort:
                        break;
                }
            }
        }

        private void init_copy(SpellList to_copy)
        {
            IsMainSpellList = to_copy.IsMainSpellList;
        }

        public SpellList(SpellList to_copy) : base(to_copy)
        {
            init_copy(to_copy);
            init_handlers();
        }

        public override object Clone()
        {
            return new SpellList(this);
        }

    }
}
