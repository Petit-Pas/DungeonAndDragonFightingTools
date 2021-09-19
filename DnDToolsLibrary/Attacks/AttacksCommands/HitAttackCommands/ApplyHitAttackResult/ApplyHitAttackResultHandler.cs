using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Entities.EntitiesCommands.DamageCommand.ApplyDamageResultList;
using System;

namespace DnDToolsLibrary.Attacks.AttacksCommands.HitAttackCommands.ApplyHitAttackResult
{
    public class ApplyHitAttackResultHandler : BaseSuperHandler<ApplyHitAttackResultCommand, NoResponse>
    {
        public override NoResponse Execute(IMediatorCommand command)
        {
            ApplyHitAttackResultCommand _command = base.cast_command(command);

            if (_command.HitAttackResult.RollResult.Hits)
            {
                apply_damage(_command);

                apply_on_hit(_command);
            }

            return MediatorCommandResponses.NoResponse;
        }

        private void apply_on_hit(ApplyHitAttackResultCommand command)
        {
            Console.WriteLine("ERROR : apply_on_hit() not implemented in ApplyHitAttackResultHandler");
        }

        private void apply_damage(ApplyHitAttackResultCommand command)
        {
            ApplyDamageResultListCommand _command = new ApplyDamageResultListCommand(command.HitAttackResult.Target, command.HitAttackResult.DamageList);
            
            _mediator.Value.Execute(_command);

            _command.AddToInnerCommands(_command);
        }
    }
}
