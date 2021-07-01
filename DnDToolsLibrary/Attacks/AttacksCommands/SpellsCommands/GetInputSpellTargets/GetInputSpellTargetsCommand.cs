using BaseToolsLibrary.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.GetInputSpellTargets
{
    public class GetInputSpellTargetsCommand : IMediatorCommand
    {
        public GetInputSpellTargetsCommand(int amountTargets, bool targetCanBeSelectedMoreThanOnce)
        {
            AmountTargets = amountTargets;
            TargetCanBeSelectedMoreThanOnce = targetCanBeSelectedMoreThanOnce;
        }

        public bool TargetCanBeSelectedMoreThanOnce { get; set; }
        public int AmountTargets { get; set; }
    }
}
