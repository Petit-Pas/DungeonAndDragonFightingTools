using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.SpellLevelQueries
{
    public class NormalSpellLevelQuery : SpellLevelBaseQuery, IUiCommand
    {
        public NormalSpellLevelQuery(int baseLevel) : base(baseLevel)
        {
        }
    }
}
