using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Fight;
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
        private Lazy<IStatusProvider> __lazyStatusProvider;

        public override NoResponse Execute(IMediatorCommand command)
        {
            LoseConcentrationCommand _command = base.castCommand(command);
            PlayableEntity entity = _command.GetEntity();

            if (!entity.IsFocused)
            {
                return MediatorCommandResponses.NoResponse;
            }

            _statusProvider.GetOnHitStatusesAppliedBy(entity.DisplayName);
            // remove the statuses that were applied by entity AND required concentration

            return MediatorCommandResponses.NoResponse;
        }
    }
}
