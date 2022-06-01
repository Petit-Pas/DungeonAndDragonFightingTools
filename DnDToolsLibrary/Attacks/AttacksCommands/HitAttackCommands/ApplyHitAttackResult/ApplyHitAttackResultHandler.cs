using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommands;
using DnDToolsLibrary.Entities.EntitiesCommands.DamageCommand.ApplyDamageResultList;
using DnDToolsLibrary.Status;
using DnDToolsLibrary.Status.StatusCommands.TryApplyStatusCommands;

namespace DnDToolsLibrary.Attacks.AttacksCommands.HitAttackCommands.ApplyHitAttackResult
{
    public class ApplyHitAttackResultHandler : SuperDndCommandHandlerBase<ApplyHitAttackResultCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(ApplyHitAttackResultCommand command)
        {
            command.AddLog("Attack: ");
            command.AddLog($"{command.HitAttackResult.RollResult} => ", FontWeightProvider.Bold);
            if (command.HitAttackResult.RollResult.Hits || command.HitAttackResult.AutomaticallyHits)
            {
                command.AddLog($"{(command.HitAttackResult.RollResult.Crits ? "Critical hit!" : "Hits.")}\r\n", FontWeightProvider.Bold, FontColorProvider.Success);
                apply_damage(command);

                apply_on_hit(command);
                return MediatorCommandStatii.Success;
            }

            command.AddLog($"{(command.HitAttackResult.RollResult.CriticalFailure ? "Critical miss!" : "Misses.")}\r\n", FontWeightProvider.Bold, FontColorProvider.Failure);
            return MediatorCommandStatii.Failed;
        }

        private void apply_on_hit(ApplyHitAttackResultCommand command)
        {
            foreach (OnHitStatus status in command.HitAttackResult.OnHitStatuses)
            {
                var statusCommand = new TryApplyStatusCommand(command.HitAttackResult.CasterName, command.HitAttackResult.TargetName, status);
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
