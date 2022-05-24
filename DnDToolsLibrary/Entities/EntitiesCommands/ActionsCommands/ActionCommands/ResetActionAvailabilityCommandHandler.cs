using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ActionCommands;

public class ResetActionAvailabilityCommandHandler : SuperDndCommandHandlerBase<ResetActionAvailabilityCommand, IMediatorCommandResponse>
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