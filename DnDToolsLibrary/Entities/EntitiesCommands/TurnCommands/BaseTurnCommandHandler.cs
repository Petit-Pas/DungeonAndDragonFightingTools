using System.Collections.Generic;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.ApplyDotCommands;
using DnDToolsLibrary.Status;
using DnDToolsLibrary.Status.StatusCommands.EndStatusCommands.ReduceRemainingRoundsCommands;
using DnDToolsLibrary.Status.StatusCommands.EndStatusCommands.RetrySavingCommands;

namespace DnDToolsLibrary.Entities.EntitiesCommands.TurnCommands
{
    public abstract class BaseTurnCommandHandler<TCommand> : BaseCommandHandlers.SuperDndCommandHandler<TCommand, IMediatorCommandResponse>
        where TCommand : SuperCommandBase 
    {
        protected static void TriggerDot(SuperCommandBase command, IEnumerable<OnHitStatus> statii, bool startOfTurn, bool casterTurn)
        {
            foreach (var status in statii)
            {
                var dotCommand = new ApplyDotCommand(status.Id, startOfTurn, casterTurn);
                Mediator.Execute(dotCommand);
                command.PushToInnerCommands(dotCommand);
            }
        }

        protected static void RetrySavings(SuperCommandBase command, IEnumerable<OnHitStatus> statii)
        {
            foreach (var status in statii)
            {
                var retrySavingCommand = new RetrySavingCommand(status.Id);
                Mediator.Execute(retrySavingCommand);
                command.PushToInnerCommands(retrySavingCommand);
            }
        }

        protected static void ReduceDuration(SuperCommandBase command, IEnumerable<OnHitStatus> statii)
        {
            foreach (var status in statii)
            {
                var reduceRemainingRoundsCommands = new ReduceRemainingRoundsCommand(status.Id);
                Mediator.Execute(reduceRemainingRoundsCommands);
                command.PushToInnerCommands(reduceRemainingRoundsCommands);
            }
        }
    }
}
