using System;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ActionCommands;

public class RemoveActionCommandHandler : DndCommandHandlerBase<RemoveActionCommand, IMediatorCommandResponse>
{
    public override IMediatorCommandResponse Execute(RemoveActionCommand command)
    {
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

    public override void Undo(RemoveActionCommand command)
    {
        if (command.CommandStatus == MediatorCommandStatii.Success)
        {
            command.GetEntity().HasAction = true;
        }
    }
}