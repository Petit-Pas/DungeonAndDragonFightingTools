using System.Collections.Generic;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommands;
using DnDToolsLibrary.Status;
using DnDToolsLibrary.Status.StatusCommands.ApplyDotCommands;
using DnDToolsLibrary.Status.StatusCommands.EndStatusCommands.ReduceRemainingRoundsCommands;
using DnDToolsLibrary.Status.StatusCommands.EndStatusCommands.RetrySavingCommands;

namespace DnDToolsLibrary.Entities.EntitiesCommands.TurnCommands
{
    public abstract class DndCommandHandlerBaseTurnCommandHandler<TCommand> : SuperDndCommandHandlerBase<TCommand, IMediatorCommandResponse>
        where TCommand : SuperDndCommandBase
    {
        protected static void TriggerDot(SuperDndCommandBase command, IEnumerable<OnHitStatus> statii, bool startOfTurn, bool casterTurn)
        {
            foreach (var status in statii)
            {
                var dotCommand = new ApplyDotCommand(status.Id, startOfTurn, casterTurn);
                Mediator.Execute(dotCommand);
                command.PushToInnerCommands(dotCommand);
            }
        }

        protected static void RetrySavings(SuperDndCommandBase command, IEnumerable<OnHitStatus> statii)
        {
            foreach (var status in statii)
            {
                var retrySavingCommand = new RetrySavingCommand(status.Id);
                Mediator.Execute(retrySavingCommand);
                command.PushToInnerCommands(retrySavingCommand);
            }
        }

        protected static void ReduceDuration(SuperDndCommandBase command, IEnumerable<OnHitStatus> statii)
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
