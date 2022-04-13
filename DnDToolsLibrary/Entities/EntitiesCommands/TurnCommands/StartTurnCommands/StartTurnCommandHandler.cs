using System;
using System.Collections.Generic;
using System.Linq;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ActionCommands;
using DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.BonusActionCommands;
using DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ReactionCommands;
using DnDToolsLibrary.Status;

namespace DnDToolsLibrary.Entities.EntitiesCommands.TurnCommands.StartTurnCommands
{
    internal class StartTurnCommandHandler : BaseTurnCommandHandler<StartTurnCommand>
    {
        public override IMediatorCommandResponse Execute(StartTurnCommand command)
        {
            ResetActions(command);

            HandleAffectingStatii(command);

            HandleAppliedStatii(command);

            return MediatorCommandStatii.NoResponse;
        }

        private void HandleAffectingStatii(StartTurnCommand command)
        {
            var affectingStatii = _statusProvider.GetOnHitStatusesAppliedOn(command.GetEntityName()).ToArray();

            TriggerDot(command, affectingStatii.Where(x => 
                x.DotDamageList.Any(dot => dot.TriggersStartOfTurn && dot.TriggersOnAffectedsTurn)),
                true, false);

            RetrySavings(command, affectingStatii.Where(x =>
                    x.CanRedoSavingThrow && x.SavingIsRemadeAtStartOfTurn));

            ReduceDuration(command, affectingStatii.Where(x =>
                x.HasAMaximumDuration && x.DurationIsBasedOnStartOfTurn && x.DurationIsCalculatedOnTargetTurn));
        }

        private void HandleAppliedStatii(StartTurnCommand command)
        {
            var appliedStatii = _statusProvider.GetOnHitStatusesAppliedBy(command.GetEntityName()).ToArray();

            TriggerDot(command, appliedStatii.Where(x =>
                x.DotDamageList.Any(dot => dot.TriggersStartOfTurn && dot.TriggersOnCastersTurn)),
                true, true);

            ReduceDuration(command, appliedStatii.Where(x =>
                x.HasAMaximumDuration && x.DurationIsBasedOnStartOfTurn && x.DurationIsCalculatedOnCasterTurn));
        }

        private void ResetActions(StartTurnCommand command)
        {
            var actionCommand = new ResetActionAvailabilityCommand(command.GetEntityName());
            var bonusActionCommand = new ResetBonusActionAvailabilityCommand(command.GetEntityName());
            var reactionCommand = new ResetReactionAvailabilityCommand(command.GetEntityName());

            this._mediator.Value.Execute(actionCommand);
            this._mediator.Value.Execute(bonusActionCommand);
            this._mediator.Value.Execute(reactionCommand);

            command.PushToInnerCommands(actionCommand);
            command.PushToInnerCommands(bonusActionCommand);
            command.PushToInnerCommands(reactionCommand);
        }

    }
}
