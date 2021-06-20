using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.HitAttacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Attacks.AttacksCommands.HitAttackCommands.ApplyHitAttackResult
{ 
    public class ApplyHitAttackResultCommand : BaseSuperCommand
    {
        public HitAttackResult HitAttackResult { get; set; }

        public ApplyHitAttackResultCommand(HitAttackResult hitAttackResult)
        {
            HitAttackResult = hitAttackResult.Clone() as HitAttackResult;
        }
    }
}
