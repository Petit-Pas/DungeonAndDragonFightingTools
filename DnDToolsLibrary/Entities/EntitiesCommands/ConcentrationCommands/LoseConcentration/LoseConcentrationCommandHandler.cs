using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.RemoveStatus;
using DnDToolsLibrary.Status;
using System;
using System.Linq;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.LoseConcentration
{
    public class LoseConcentrationCommandHandler : SuperCommandHandlerBase<LoseConcentrationCommand, IMediatorCommandResponse>
    {
        private IStatusProvider _statusProvider { get => __lazyStatusProvider.Value; }
        private Lazy<IStatusProvider> __lazyStatusProvider = new Lazy<IStatusProvider>(() => DIContainer.GetImplementation<IStatusProvider>());

        public override IMediatorCommandResponse Execute(LoseConcentrationCommand command)
        {
            PlayableEntity entity = command.GetEntity();

            if (!entity.IsFocused)
            {
                return MediatorCommandStatii.Canceled;
            }
            command.WasFocused = true;
            entity.IsFocused = false;

            // removes the statuses that were applied by entity AND required concentration
            var statuses = _statusProvider.GetOnHitStatusesAppliedBy(entity.DisplayName).Where(x => x.EndsOnCasterLossOfConcentration).ToList();
            foreach (OnHitStatus status in statuses)
            {
                RemoveStatusCommand innerCommand = new RemoveStatusCommand(status.Id, status.Target.DisplayName);
                command.PushToInnerCommands(innerCommand);
                _mediator.Value.Execute(innerCommand);
            }
            return MediatorCommandStatii.Success;
        }

        public override void Undo(LoseConcentrationCommand command)
        {
            PlayableEntity entity = command.GetEntity();

            entity.IsFocused = command.WasFocused;
        }
    }
}
