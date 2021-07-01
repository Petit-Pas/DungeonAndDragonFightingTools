using BaseToolsLibrary.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.GetInputSpellTargets
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
