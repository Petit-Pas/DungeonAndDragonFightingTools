using System;
using System.Linq;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Fight.Events;

namespace DnDToolsLibrary.Entities.EntitiesCommands.TurnCommands.EndTurnCommands
{
    public class EndTurnCommandHandler : BaseTurnCommandHandler<EndTurnCommand>
    {
        private static Lazy<ITurnManager> _lazyTurnManager = new(DIContainer.GetImplementation<ITurnManager>);
        private static ITurnManager _turnManager => _lazyTurnManager.Value;

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
            var eventArgs = new TurnEndedEventArgs(entity.DisplayName);

            // notifying through the entity itself => specified for controls that have only this entity as DataContext
            entity.InvokeTurnEnded(eventArgs);

            // notifying through the turnManager => broader for controls that have all fighters as DataContext
            _turnManager.InvokeTurnEnded(eventArgs);
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

        public override void Undo(EndTurnCommand command)
        {
            base.Undo(command);

            var entity = command.GetEntity();
            var eventArgs = new TurnStartedEventArgs(entity.DisplayName);

            // notifying through the entity itself => specified for controls that have only this entity as DataContext
            entity.InvokeTurnStarted(eventArgs);

            // notifying through the turnManager => broader for controls that have all fighters as DataContext
            _turnManager.InvokeTurnStarted(eventArgs);
        }
    }
}
