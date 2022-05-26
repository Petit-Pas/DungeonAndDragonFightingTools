using System;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ActionCommands;

public class AddActionCommandHandler : DndCommandHandlerBase<AddActionCommand, IMediatorCommandResponse>
{
    public override IMediatorCommandResponse Execute(AddActionCommand command)
    {
        var target = command.GetEntity();

        if (!target.HasAction)
        {
            target.HasAction = true;
            command.CommandStatus = MediatorCommandStatii.Success;
        }
        else
        {
            Console.WriteLine($"WARN: trying to add action for {target.DisplayName} while he has one already");
            command.CommandStatus = MediatorCommandStatii.Canceled;
        }

        return command.CommandStatus;
    }

    public override void Undo(AddActionCommand command)
    {
        base.Undo(command);
        if (command.CommandStatus == MediatorCommandStatii.Success)
        {
            command.GetEntity().HasAction = false;
        }
    }
}