using BaseToolsLibrary.Mediator;
using System.Collections.Generic;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.SpellTargetsQueries
{
    public class GetInputSpellTargetsResponse : IMediatorCommandResponse
    {
        public GetInputSpellTargetsResponse(List<string> targetNames)
        {
            TargetNames = targetNames;
        }
        private GetInputSpellTargetsResponse() { }

        public List<string> TargetNames;
    }
}
