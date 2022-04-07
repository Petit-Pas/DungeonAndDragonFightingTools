using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;
using System;
using System.Linq;

namespace DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.RemoveStatus
{
    public class RemoveStatusCommandHandler : BaseMediatorHandler<RemoveStatusCommand, IMediatorCommandResponse>
    {
        private IStatusProvider _statusProvider { get => _lazyStatusProvider.Value; }
        private Lazy<IStatusProvider> _lazyStatusProvider = new Lazy<IStatusProvider>(() => DIContainer.GetImplementation<IStatusProvider>());

        public override IMediatorCommandResponse Execute(RemoveStatusCommand command)
        {
            PlayableEntity target = command.GetEntity();

            _statusProvider.Remove(command.Status);
            StatusReference statusReference = target.AffectingStatusList.First(x => x.ActualStatusReferenceId == command.Status.Id);
            target.AffectingStatusList.Remove(statusReference);

            return MediatorCommandStatii.Success;
        }

        public override void Undo(RemoveStatusCommand command)
        {
            PlayableEntity target = command.GetEntity();

            _statusProvider.Add(command.Status);
            target.AffectingStatusList.Add(new StatusReference(command.Status));
        }
    }
}
