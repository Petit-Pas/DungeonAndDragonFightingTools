using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.RemoveStatus;
using DnDToolsLibrary.Fight;

namespace DnDToolsLibrary.Status.StatusCommands.EndStatusCommands.ReduceRemainingRoundsCommands
{
    public class ReduceRemainingRoundsCommandHandler : SuperCommandHandlerBase<ReduceRemainingRoundsCommand, IMediatorCommandResponse>
    {
        private static readonly Lazy<IStatusProvider> _lazyStatusProvider = new Lazy<IStatusProvider>(DIContainer.GetImplementation<IStatusProvider>);
        private static IStatusProvider _statusProvider => _lazyStatusProvider.Value;

        public override IMediatorCommandResponse Execute(IMediatorCommand genericCommand)
        {
            var command = base.castCommand(genericCommand);
            var status = _statusProvider.GetOnHitStatusById(command.StatusId);

            if (status != null)
            {
                status.RemainingRounds -= 1;
                if (status.RemainingRounds <= 0)
                {
                    var removeStatusCommand = new RemoveStatusCommand(status.Id, status.TargetName);
                    
                    command.PushToInnerCommands(removeStatusCommand);
                    base._mediator.Value.Execute(removeStatusCommand);
                }
                return MediatorCommandStatii.Success;
            }

            return MediatorCommandStatii.Canceled;
        }

        public override void Undo(IMediatorCommand genericCommand)
        {
            base.Undo(genericCommand);
            var command = base.castCommand(genericCommand);
            var status = _statusProvider.GetOnHitStatusById(command.StatusId);

            if (status != null)
            {
                status.RemainingRounds += 1;
            }
        }
    }
}
