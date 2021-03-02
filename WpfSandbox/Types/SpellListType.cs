using DDFight;
using DDFight.Game.Aggression.Spells;
using DDFight.Tools.Save;
using System;

namespace WpfSandbox.Types
{
    public class SpellListType : GenericList<SpellType>
    {

        public static SpellList Convert(SpellListType list)
        {
            SpellList result = new SpellList();
            foreach (SpellType spell in list.Elements)
                result.AddElementSilent(SpellType.Convert(spell));
            return result;
        }


        public SpellListType()
        {
            IsMainSpellList = false;
            init_handlers();
        }

        public void init_handlers()
        {
        }

        public SpellListType(bool isMainSpellList = false)
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
                            SaveManager.SaveUnique<SpellType>(e.Element);
                        break;
                    case GenericListOperation.Deletion:
                        if (IsMainSpellList)
                            SaveManager.DeleteUnique<SpellType>(e.Element);
                        break;
                    case GenericListOperation.Modification:
                        if (IsMainSpellList)
                            SaveManager.SaveUnique<SpellType>(e.Element);
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

        private void init_copy(SpellListType to_copy)
        {
            IsMainSpellList = to_copy.IsMainSpellList;
        }

        public SpellListType(SpellListType to_copy) : base(to_copy)
        {
            init_copy(to_copy);
            init_handlers();
        }

        public override object Clone()
        {
            return new SpellListType(this);
        }

    }
}
