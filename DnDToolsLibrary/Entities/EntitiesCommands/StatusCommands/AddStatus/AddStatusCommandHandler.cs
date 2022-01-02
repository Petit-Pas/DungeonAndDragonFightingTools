using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using BaseToolsLibrary.Mediator.CommandStatii;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.AddStatus
{
    public class AddStatusCommandHandler : BaseMediatorHandler<AddStatusCommand, MediatorCommandNoResponse>
    {
        private Lazy<IStatusProvider> _statusProvider = new Lazy<IStatusProvider>(() => DIContainer.GetImplementation<IStatusProvider>());

        public override MediatorCommandNoResponse Execute(IMediatorCommand command)
        {
            AddStatusCommand _command = this.castCommand(command);
            PlayableEntity target = _command.GetEntity();

            _command.Status.Affected = target;
            _statusProvider.Value.Add(_command.Status);
            target.AffectingStatusList.Add(new StatusReference(_command.Status));

            return MediatorCommandStatii.NoResponse;
        }

        public override void Undo(IMediatorCommand command)
        {
            AddStatusCommand _command = this.castCommand(command);
            PlayableEntity target = _command.GetEntity();

            _statusProvider.Value.Remove(_command.Status);
            StatusReference statusReference = target.AffectingStatusList.First(x => x.ActualStatusReferenceId == _command.Status.Id);
            target.AffectingStatusList.Remove(statusReference);
        }
    }
}
