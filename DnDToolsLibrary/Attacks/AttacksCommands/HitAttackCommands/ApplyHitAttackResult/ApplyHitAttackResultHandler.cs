using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommandHandlers;
using DnDToolsLibrary.Entities.EntitiesCommands.DamageCommand.ApplyDamageResultList;
using DnDToolsLibrary.Status;
using DnDToolsLibrary.Status.StatusCommands.TryApplyStatusCommands;

namespace DnDToolsLibrary.Attacks.AttacksCommands.HitAttackCommands.ApplyHitAttackResult
{
    public class ApplyHitAttackResultHandler : SuperDndCommandHandler<ApplyHitAttackResultCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(ApplyHitAttackResultCommand command)
        {

            if (command.HitAttackResult.RollResult.Hits || command.HitAttackResult.AutomaticallyHits)
            {
                apply_damage(command);

                apply_on_hit(command);
                return MediatorCommandStatii.Success;
            }

            return MediatorCommandStatii.Failed;
        }

        private void apply_on_hit(ApplyHitAttackResultCommand command)
        {
            foreach (OnHitStatus status in command.HitAttackResult.OnHitStatuses)
            {
                var statusCommand = new TryApplyStatusCommand(command.HitAttackResult.OwnerName, command.HitAttackResult.TargetName, status);
                Mediator.Execute(statusCommand);
                command.PushToInnerCommands(statusCommand);
            }
        }

        private void apply_damage(ApplyHitAttackResultCommand command)
        {
            ApplyDamageResultListCommand _command = new ApplyDamageResultListCommand(command.HitAttackResult.Target, command.HitAttackResult.DamageList);

            Mediator.Execute(_command);

            _command.PushToInnerCommands(_command);
        }
    }
}
