using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.LoseConcentration
{
    public class LoseConcentrationCommandHandler : SuperCommandHandlerBase<LoseConcentrationCommand, NoResponse>
    {
        private IStatusProvider _statusProvider { get => __lazyStatusProvider.Value; }
        private Lazy<IStatusProvider> __lazyStatusProvider = new Lazy<IStatusProvider>(() => DIContainer.GetImplementation<IStatusProvider>());

        public override NoResponse Execute(IMediatorCommand command)
        {
            LoseConcentrationCommand _command = base.castCommand(command);
            PlayableEntity entity = _command.GetEntity();

            if (!entity.IsFocused)
            {
                return MediatorCommandResponses.NoResponse;
            }

            // removes the statuses that were applied by entity AND required concentration
            var statuses = _statusProvider.GetOnHitStatusesAppliedBy(entity.DisplayName).Where(x => x.EndsOnCasterLossOfConcentration);
            foreach (OnHitStatus status in statuses)
            {
                //TODO this should be in a command by itself

                // remove from status provider
                _statusProvider.Remove(status);
                // remove from affected
                StatusReference reference = status.Affected.AffectingStatusList.First(x => x.ActualStatusReferenceId == status.Id);
                status.Affected.AffectingStatusList.Remove(reference);
            }
            return MediatorCommandResponses.NoResponse;
        }
    }
}
