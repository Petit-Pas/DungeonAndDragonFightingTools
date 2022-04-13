using System;
using System.Collections.Generic;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.ApplyDotCommands;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;
using DnDToolsLibrary.Status.StatusCommands.EndStatusCommands.ReduceRemainingRoundsCommands;
using DnDToolsLibrary.Status.StatusCommands.EndStatusCommands.RetrySavingCommands;

namespace DnDToolsLibrary.Entities.EntitiesCommands.TurnCommands
{
    public abstract class BaseTurnCommandHandler<TCommand> : SuperCommandHandlerBase<TCommand, IMediatorCommandResponse> 
        where TCommand : SuperCommandBase 
    {
        private static readonly Lazy<IStatusProvider> _lazyStatusProvider = new(DIContainer.GetImplementation<IStatusProvider>);

        protected static IStatusProvider _statusProvider => _lazyStatusProvider.Value;

        protected void TriggerDot(SuperCommandBase command, IEnumerable<OnHitStatus> statii, bool startOfTurn, bool casterTurn)
        {
            foreach (var status in statii)
            {
                var dotCommand = new ApplyDotCommand(status.Id, startOfTurn, casterTurn);
                _mediator.Value.Execute(dotCommand);
                command.PushToInnerCommands(dotCommand);
            }
        }

        protected void RetrySavings(SuperCommandBase command, IEnumerable<OnHitStatus> statii)
        {
            foreach (var status in statii)
            {
                var retrySavingCommand = new RetrySavingCommand(status.Id);
                _mediator.Value.Execute(retrySavingCommand);
                command.PushToInnerCommands(retrySavingCommand);
            }
        }

        protected void ReduceDuration(SuperCommandBase command, IEnumerable<OnHitStatus> statii)
        {
            foreach (var status in statii)
            {
                var reduceRemainingRoundsCommands = new ReduceRemainingRoundsCommand(status.Id);
                _mediator.Value.Execute(reduceRemainingRoundsCommands);
                command.PushToInnerCommands(reduceRemainingRoundsCommands);
            }
        }
    }
}
