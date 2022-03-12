using BaseToolsLibrary.Mediator;
using BaseToolsLibrary.Mediator.CommandStatii;
using DnDToolsLibrary.Entities.EntitiesCommands.DamageCommand.ApplyDamageResultList;
using System;
using DnDToolsLibrary.Status;
using DnDToolsLibrary.Status.StatusCommands.TryApplyStatusCommands;

namespace DnDToolsLibrary.Attacks.AttacksCommands.HitAttackCommands.ApplyHitAttackResult
{
    public class ApplyHitAttackResultHandler : SuperCommandHandlerBase<ApplyHitAttackResultCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(IMediatorCommand genericCommand)
        {
            ApplyHitAttackResultCommand _command = base.castCommand(genericCommand);

            if (_command.HitAttackResult.RollResult.Hits || _command.HitAttackResult.AutomaticallyHits)
            {
                apply_damage(_command);

                apply_on_hit(_command);
                return MediatorCommandStatii.Success;
            }

            return MediatorCommandStatii.Failed;
        }

        private void apply_on_hit(ApplyHitAttackResultCommand command)
        {
            foreach (OnHitStatus status in command.HitAttackResult.OnHitStatuses)
            {
                var statusCommand = new TryApplyStatusCommand(command.HitAttackResult.OwnerName, command.HitAttackResult.TargetName, status);
                base._mediator.Value.Execute(statusCommand);
                command.PushToInnerCommands(statusCommand);
            }
        }

        private void apply_damage(ApplyHitAttackResultCommand command)
        {
            ApplyDamageResultListCommand _command = new ApplyDamageResultListCommand(command.HitAttackResult.Target, command.HitAttackResult.DamageList);
            
            _mediator.Value.Execute(_command);

            _command.PushToInnerCommands(_command);
        }
    }
}
