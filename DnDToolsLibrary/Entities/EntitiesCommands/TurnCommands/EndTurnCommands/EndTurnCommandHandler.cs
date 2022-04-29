﻿using System.Linq;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Fight.Events;

namespace DnDToolsLibrary.Entities.EntitiesCommands.TurnCommands.EndTurnCommands
{
    public class EndTurnCommandHandler : BaseTurnCommandHandler<EndTurnCommand>
    {
        public override IMediatorCommandResponse Execute(EndTurnCommand command)
        {
            HandleAffectingStatii(command);
            HandleAppliedStatii(command);

            NotifyEndOfTurn(command);

            return MediatorCommandStatii.NoResponse;
        }

        private static void NotifyEndOfTurn(EndTurnCommand command)
        {
            var entity = command.GetEntity();

            entity.InvokeTurnEnded(new TurnEndedEventArgs(entity.DisplayName));
        }

        private static void HandleAffectingStatii(EndTurnCommand command)
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

        private static void HandleAppliedStatii(EndTurnCommand command)
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
