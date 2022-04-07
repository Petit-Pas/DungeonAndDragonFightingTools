using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ActionCommands;

public class ResetActionAvailabilityCommandHandler : SuperCommandHandlerBase<ResetActionAvailabilityCommand, IMediatorCommandResponse>
{
    public override IMediatorCommandResponse Execute(ResetActionAvailabilityCommand command)
    {
        var target = command.GetEntity();

        if (!target.HasAction)
        {
            var innerCommand = new AddActionCommand(target.DisplayName);
            command.PushToInnerCommands(innerCommand);
            _mediator.Value.Execute(innerCommand);
            return MediatorCommandStatii.Success;
        }
        return MediatorCommandStatii.Canceled;
    }
}