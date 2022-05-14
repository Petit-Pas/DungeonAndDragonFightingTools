using System;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.RemoveStatus;

namespace DnDToolsLibrary.Status.StatusCommands.EndStatusCommands.ReduceRemainingRoundsCommands
{
    public class ReduceRemainingRoundsCommandHandler : SuperCommandHandlerBase<ReduceRemainingRoundsCommand, IMediatorCommandResponse>
    {
        private static readonly Lazy<IStatusProvider> _lazyStatusProvider = new Lazy<IStatusProvider>(DIContainer.GetImplementation<IStatusProvider>);
        private static IStatusProvider _statusProvider => _lazyStatusProvider.Value;

        public override IMediatorCommandResponse Execute(ReduceRemainingRoundsCommand command)
        {
            var status = _statusProvider.GetOnHitStatusById(command.StatusId);

            if (status != null)
            {
                status.RemainingRounds -= 1;
                if (status.RemainingRounds <= 0)
                {
                    var removeStatusCommand = new RemoveStatusCommand(status.Id, status.TargetName);

                    command.PushToInnerCommands(removeStatusCommand);
                    _mediator.Value.Execute(removeStatusCommand);
                }
                return MediatorCommandStatii.Success;
            }

            return MediatorCommandStatii.Canceled;
        }

        public override void Undo(ReduceRemainingRoundsCommand command)
        {
            base.Undo(command);
            var status = _statusProvider.GetOnHitStatusById(command.StatusId);

            if (status != null)
            {
                status.RemainingRounds += 1;
            }
        }
    }
}
