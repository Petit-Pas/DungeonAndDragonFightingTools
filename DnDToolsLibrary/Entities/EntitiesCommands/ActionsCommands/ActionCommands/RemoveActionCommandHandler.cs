using System;
using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ActionCommands;

public class RemoveActionCommandHandler : BaseMediatorHandler<RemoveActionCommand, IMediatorCommandResponse>
{
    public override IMediatorCommandResponse Execute(IMediatorCommand genericCommand)
    {
        var command = base.castCommand(genericCommand);
        var target = command.GetEntity();

        if (target.HasAction)
        {
            target.HasAction = false;
            command.CommandStatus = MediatorCommandStatii.Success;
        }
        else
        {
            Console.WriteLine($"WARN: trying to remove action for {target.DisplayName} while he has none");
            command.CommandStatus = MediatorCommandStatii.Canceled;
        }

        return command.CommandStatus;
    }

    public override void Undo(IMediatorCommand genericCommand)
    {
        var command = base.castCommand(genericCommand);

        if (command.CommandStatus == MediatorCommandStatii.Success)
        {
            command.GetEntity().HasAction = true;
        }
    }
}