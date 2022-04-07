using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ActionCommands;

public class InvertActionAvailabilityCommandHandler : SuperCommandHandlerBase<InvertActionAvailabilityCommand, IMediatorCommandResponse>
{
    public override IMediatorCommandResponse Execute(InvertActionAvailabilityCommand command)
    {
        IMediatorCommand innerCommand;
        var target = command.GetEntity();

        if (target.HasAction)
        {
            innerCommand = new RemoveActionCommand(target.DisplayName);
        }
        else
        {
            innerCommand = new AddActionCommand(target.DisplayName);
        }

        command.PushToInnerCommands(innerCommand);
        _mediator.Value.Execute(innerCommand);

        return MediatorCommandStatii.NoResponse;
    }
}