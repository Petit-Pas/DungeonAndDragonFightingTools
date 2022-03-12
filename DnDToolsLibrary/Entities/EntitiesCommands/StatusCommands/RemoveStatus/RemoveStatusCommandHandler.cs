using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.RemoveStatus
{
    public class RemoveStatusCommandHandler : BaseMediatorHandler<RemoveStatusCommand, IMediatorCommandResponse>
    {
        private IStatusProvider _statusProvider { get => _lazyStatusProvider.Value; }
        private Lazy<IStatusProvider> _lazyStatusProvider = new Lazy<IStatusProvider>(() => DIContainer.GetImplementation<IStatusProvider>());

        public override IMediatorCommandResponse Execute(IMediatorCommand genericCommand)
        {
            RemoveStatusCommand _command = this.castCommand(genericCommand);
            PlayableEntity target = _command.GetEntity();

            _statusProvider.Remove(_command.Status);
            StatusReference statusReference = target.AffectingStatusList.First(x => x.ActualStatusReferenceId == _command.Status.Id);
            target.AffectingStatusList.Remove(statusReference);

            return MediatorCommandStatii.Success;
        }

        public override void Undo(IMediatorCommand genericCommand)
        {
            RemoveStatusCommand _command = this.castCommand(genericCommand);
            PlayableEntity target = _command.GetEntity();

            _statusProvider.Add(_command.Status);
            target.AffectingStatusList.Add(new StatusReference(_command.Status));
        }
    }
}
