using System.Linq;
using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Entities.EntitiesCommands.TurnCommands.EndTurnCommands
{
    public class EndTurnCommandHandler : BaseTurnCommandHandler<EndTurnCommand>
    {
        public override IMediatorCommandResponse Execute(EndTurnCommand command)
        {
            HandleAffectingStatii(command);
            HandleAppliedStatii(command);

            return MediatorCommandStatii.NoResponse;
        }

        private void HandleAffectingStatii(EndTurnCommand command)
        {
            var affectingStatii = _statusProvider.GetOnHitStatusesAppliedOn(command.GetEntityName()).ToArray();

            TriggerDot(command, affectingStatii.Where(x =>
                    x.DotDamageList.Any(dot => dot.TriggersEndOfTurn && dot.TriggersOnAffectedsTurn)),
                false, false);

            RetrySavings(command, affectingStatii.Where(x =>
                x.CanRedoSavingThrow && x.SavingIsRemadeAtEndOfTurn));

            ReduceDuration(command, affectingStatii.Where(x =>
                x.HasAMaximumDuration && x.DurationIsBasedOnEndOfTurn && x.DurationIsCalculatedOnTargetTurn));
        }

        private void HandleAppliedStatii(EndTurnCommand command)
        {
            var appliedStatii = _statusProvider.GetOnHitStatusesAppliedBy(command.GetEntityName()).ToArray();

            TriggerDot(command, appliedStatii.Where(x =>
                    x.DotDamageList.Any(dot => dot.TriggersEndOfTurn && dot.TriggersOnCastersTurn)),
                false, true);

            ReduceDuration(command, appliedStatii.Where(x =>
                x.HasAMaximumDuration && x.DurationIsBasedOnEndOfTurn && x.DurationIsCalculatedOnCasterTurn));
        }
    }
}
