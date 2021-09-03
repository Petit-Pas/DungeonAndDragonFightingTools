using BaseToolsLibrary.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.GetInputSpellLevel
{
    public class GetInputSpellLevelResponse : IMediatorCommandResponse
    {
        public GetInputSpellLevelResponse(int level)
        {
            Level = level;
        }
        private GetInputSpellLevelResponse() { }

        public int Level { get; set; }
    }
}
