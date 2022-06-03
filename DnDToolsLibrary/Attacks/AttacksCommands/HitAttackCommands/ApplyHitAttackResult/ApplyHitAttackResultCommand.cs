using DnDToolsLibrary.Attacks.HitAttacks;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Attacks.AttacksCommands.HitAttackCommands.ApplyHitAttackResult
{
    public class ApplyHitAttackResultCommand : SuperDndCommandBase
    {

        public ApplyHitAttackResultCommand(HitAttackResult hitAttackResult, bool isComingFromASpell = true)
        {
            HitAttackResult = hitAttackResult.Clone() as HitAttackResult;
            IsComingFromASpell = isComingFromASpell;
        }
        public HitAttackResult HitAttackResult { get; set; }

        // if set to true, we don't need to log any context, just the result of the attack. Otherwise, we do give context because this is the main command.
        public bool IsComingFromASpell { get; set; }
    }
}
