using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.HitAttacks;

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
