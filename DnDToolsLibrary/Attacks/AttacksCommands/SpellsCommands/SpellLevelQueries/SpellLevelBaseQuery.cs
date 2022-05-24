using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.SpellLevelQueries
{
    public abstract class SpellLevelBaseQuery : DndCommandBase
    {
        public int BaseLevel = -1;

        public SpellLevelBaseQuery(int base_level)
        {
            BaseLevel = base_level;
        }
    }
}
