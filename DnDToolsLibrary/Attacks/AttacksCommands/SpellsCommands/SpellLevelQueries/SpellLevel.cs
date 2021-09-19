using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.SpellLevelQueries
{
    public class SpellLevel : IMediatorCommandResponse
    {
        public SpellLevel(int level)
        {
            Value = level;
        }
        private SpellLevel() { }

        public int Value { get; set; }
    }
}
