using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.SpellLevelQueries
{
    public class CantripLevelQuery : SpellLevelBaseQuery, IMediatorCommand, IUiCommand
    {
        public CantripLevelQuery() : base(1)
        {

        }
    }
}
