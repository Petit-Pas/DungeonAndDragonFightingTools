using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommandHandlers;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ActionCommands;

public class InvertActionAvailabilityCommandHandler : SuperDndCommandHandler<InvertActionAvailabilityCommand, IMediatorCommandResponse>
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
        Mediator.Execute(innerCommand);

        return MediatorCommandStatii.NoResponse;
    }
}