using BaseToolsLibrary.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.GetInputSpellLevel
{
    public class GetInputNormalSpellLevelCommand : GetInputSpellLevelBaseCommand, IMediatorCommand, IUiCommand
    {
        public int BaseLevel = -1;

        public GetInputNormalSpellLevelCommand(int base_level)
        {
            BaseLevel = base_level;
        }
    }
}
