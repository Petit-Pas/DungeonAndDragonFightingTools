using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.SpellLevelQueries
{
    public class NormalSpellLevelQuery : SpellLevelBaseQuery, IMediatorCommand, IUiCommand
    {
        public NormalSpellLevelQuery(int baseLevel) : base(baseLevel)
        {
        }
    }
}
