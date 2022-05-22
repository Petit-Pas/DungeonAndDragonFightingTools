using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommandHandlers;
using DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.RemoveStatus;

namespace DnDToolsLibrary.Status.StatusCommands.EndStatusCommands.ReduceRemainingRoundsCommands
{
    public class ReduceRemainingRoundsCommandHandler : SuperDndCommandHandler<ReduceRemainingRoundsCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(ReduceRemainingRoundsCommand command)
        {
            var status = StatusProvider.GetOnHitStatusById(command.StatusId);

            if (status != null)
            {
                status.RemainingRounds -= 1;
                if (status.RemainingRounds <= 0)
                {
                    var removeStatusCommand = new RemoveStatusCommand(status.Id, status.TargetName);

                    command.PushToInnerCommands(removeStatusCommand);
                    Mediator.Execute(removeStatusCommand);
                }
                return MediatorCommandStatii.Success;
            }

            return MediatorCommandStatii.Canceled;
        }

        public override void Undo(ReduceRemainingRoundsCommand command)
        {
            base.Undo(command);
            var status = StatusProvider.GetOnHitStatusById(command.StatusId);

            if (status != null)
            {
                status.RemainingRounds += 1;
            }
        }
    }
}
