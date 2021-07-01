using BaseToolsLibrary.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.GetInputSpellLevel
{
    public class GetInputCantripLevelResponse : IMediatorCommandResponse
    {
        public GetInputCantripLevelResponse(int level)
        {
            Level = level;
        }
        private GetInputCantripLevelResponse() { }

        public int Level { get; set; }
    }
}
