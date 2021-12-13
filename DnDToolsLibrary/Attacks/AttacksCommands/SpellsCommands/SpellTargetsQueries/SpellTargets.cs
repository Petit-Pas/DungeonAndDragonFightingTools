using BaseToolsLibrary.Mediator;
using System.Collections.Generic;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.SpellTargetsQueries
{
    public class SpellTargets : IMediatorCommandResponse
    {
        public SpellTargets(List<string> targetNames)
        {
            TargetNames = targetNames;
        }
        private SpellTargets() { }

        public List<string> TargetNames;
    }
}
