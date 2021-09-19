using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.SpellLevelQueries
{
    public class NormalSpellLevelQuery : SpellLevelBaseQuery, IMediatorCommand, IUiCommand
    {
        public int BaseLevel = -1;

        public NormalSpellLevelQuery(int base_level)
        {
            BaseLevel = base_level;
        }
    }
}
