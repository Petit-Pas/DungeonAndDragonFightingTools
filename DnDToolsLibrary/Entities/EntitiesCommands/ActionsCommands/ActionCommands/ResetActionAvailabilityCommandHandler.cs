using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommandHandlers;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ActionCommands;

public class ResetActionAvailabilityCommandHandler : SuperDndCommandHandler<ResetActionAvailabilityCommand, IMediatorCommandResponse>
{
    public override IMediatorCommandResponse Execute(ResetActionAvailabilityCommand command)
    {
        var target = command.GetEntity();

        if (!target.HasAction)
        {
            var innerCommand = new AddActionCommand(target.DisplayName);
            command.PushToInnerCommands(innerCommand);
            Mediator.Execute(innerCommand);
            return MediatorCommandStatii.Success;
        }
        return MediatorCommandStatii.Canceled;
    }
}