using System.Linq;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ActionCommands;
using DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.BonusActionCommands;
using DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ReactionCommands;
using DnDToolsLibrary.Fight.Events;

namespace DnDToolsLibrary.Entities.EntitiesCommands.TurnCommands.StartTurnCommands
{
    internal class StartTurnCommandHandler : DndCommandHandlerBaseTurnCommandHandler<StartTurnCommand>
    {
        public override IMediatorCommandResponse Execute(StartTurnCommand command)
        {
            FightConsole.NewParagraph();
            FightConsole.AddEntry(command.GetEntityName(), FontWeightProvider.Bold, FontColorProvider.GetDefault(), 20);
            FightConsole.AddEntry(" starts its turn!\r\n", FontWeightProvider.Normal, FontColorProvider.GetDefault(), 20);

            NotifyStartOfTurn(command);

            ResetActions(command);

            HandleAffectingStatii(command);

            HandleAppliedStatii(command);

            return MediatorCommandStatii.NoResponse;
        }

        private void NotifyStartOfTurn(StartTurnCommand command)
        {
            var entity = command.GetEntity();
            var eventArgs = new TurnStartedEventArgs(entity.DisplayName);

            // notifying through the entity itself => specified for controls that have only this entity as DataContext
            entity.InvokeTurnStarted(eventArgs);

            // notifying through the turnManager => broader for controls that have all fighters as DataContext
            TurnManager.InvokeTurnStarted(eventArgs);

            FightersProvider.InvokeFighterSelected(new FighterSelectedEventArgs(entity.DisplayName));
        }

        private void HandleAffectingStatii(StartTurnCommand command)
        {
            var affectingStatii = StatusProvider.GetOnHitStatusesAppliedOn(command.GetEntityName()).ToArray();

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
            var appliedStatii = StatusProvider.GetOnHitStatusesAppliedBy(command.GetEntityName()).ToArray();

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

            Mediator.Execute(actionCommand);
            Mediator.Execute(bonusActionCommand);
            Mediator.Execute(reactionCommand);

            command.PushToInnerCommands(actionCommand);
            command.PushToInnerCommands(bonusActionCommand);
            command.PushToInnerCommands(reactionCommand);
        }

        public override void Undo(StartTurnCommand command)
        {
            base.Undo(command);

            var entity = command.GetEntity();
            var eventArgs = new TurnEndedEventArgs(entity.DisplayName);

            // notifying through the entity itself => specified for controls that have only this entity as DataContext
            entity.InvokeTurnEnded(eventArgs);

            // notifying through the turnManager => broader for controls that have all fighters as DataContext
            TurnManager.InvokeTurnEnded(eventArgs);

            FightConsole.RemoveLastParagraph();
        }
    }
}
