using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.HitAttacks;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Attacks.AttacksCommands.HitAttackCommands.ApplyHitAttackResult
{
    public class ApplyHitAttackResultCommand : SuperDndCommandBase
    {
        public HitAttackResult HitAttackResult { get; set; }

        public ApplyHitAttackResultCommand(HitAttackResult hitAttackResult)
        {
            HitAttackResult = hitAttackResult.Clone() as HitAttackResult;
        }
    }
}
