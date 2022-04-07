using System;
using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ActionCommands;

public class AddActionCommandHandler : BaseMediatorHandler<AddActionCommand, IMediatorCommandResponse>
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
        if (command.CommandStatus == MediatorCommandStatii.Success)
        {
            command.GetEntity().HasAction = false;
        }
    }
}