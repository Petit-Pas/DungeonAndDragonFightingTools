using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using BaseToolsLibrary.Mediator.CommandStatii;
using DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.RemoveStatus;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.LoseConcentration
{
    public class LoseConcentrationCommandHandler : SuperCommandHandlerBase<LoseConcentrationCommand, IMediatorCommandResponse>
    {
        private IStatusProvider _statusProvider { get => __lazyStatusProvider.Value; }
        private Lazy<IStatusProvider> __lazyStatusProvider = new Lazy<IStatusProvider>(() => DIContainer.GetImplementation<IStatusProvider>());

        public override IMediatorCommandResponse Execute(IMediatorCommand genericCommand)
        {
            LoseConcentrationCommand _command = base.castCommand(genericCommand);
            PlayableEntity entity = _command.GetEntity();

            if (!entity.IsFocused)
            {
                return MediatorCommandStatii.Canceled;
            }
            _command.WasFocused = true;
            entity.IsFocused = false;

            // removes the statuses that were applied by entity AND required concentration
            var statuses = _statusProvider.GetOnHitStatusesAppliedBy(entity.DisplayName).Where(x => x.EndsOnCasterLossOfConcentration).ToList();
            foreach (OnHitStatus status in statuses)
            {
                RemoveStatusCommand innerCommand = new RemoveStatusCommand(status.Id, status.Target.DisplayName);
                _command.PushToInnerCommands(innerCommand);
                _mediator.Value.Execute(innerCommand);
            }
            return MediatorCommandStatii.Success;
        }

        public override void Undo(IMediatorCommand genericCommand)
        {
            LoseConcentrationCommand _command = base.castCommand(genericCommand);
            PlayableEntity entity = _command.GetEntity();

            entity.IsFocused = _command.WasFocused;
        }
    }
}
