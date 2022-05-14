using System;
using System.Linq;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ActionCommands;
using DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.BonusActionCommands;
using DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ReactionCommands;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Fight.Events;

namespace DnDToolsLibrary.Entities.EntitiesCommands.TurnCommands.StartTurnCommands
{
    internal class StartTurnCommandHandler : BaseTurnCommandHandler<StartTurnCommand>
    {
        private static Lazy<ITurnManager> _lazyTurnManager = new(DIContainer.GetImplementation<ITurnManager>);
        private static ITurnManager _turnManager => _lazyTurnManager.Value;

        private static Lazy<IFightersProvider> _lazyFighterProvider = new(DIContainer.GetImplementation<IFightersProvider>);
        private static IFightersProvider _fighterProvider => _lazyFighterProvider.Value;

        private static ICustomConsole console = DIContainer.GetImplementation<ICustomConsole>();
        private static IFontWeightProvider fontWeightProvider = DIContainer.GetImplementation<IFontWeightProvider>();
        private static IFontColorProvider fontColorProvider = DIContainer.GetImplementation<IFontColorProvider>();


        public override IMediatorCommandResponse Execute(StartTurnCommand command)
        {
            console.NewParagraph();
            console.AddEntry(command.GetEntityName(), fontWeightProvider.Bold, fontColorProvider.GetDefault(), 20);
            console.AddEntry(" starts its turn!\r\n", fontWeightProvider.Normal, fontColorProvider.GetDefault(), 20);

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
            _turnManager.InvokeTurnStarted(eventArgs);

            _fighterProvider.InvokeFighterSelected(new FighterSelectedEventArgs(entity.DisplayName));
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

            _mediator.Value.Execute(actionCommand);
            _mediator.Value.Execute(bonusActionCommand);
            _mediator.Value.Execute(reactionCommand);

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
            _turnManager.InvokeTurnEnded(eventArgs);
        }
    }
}
