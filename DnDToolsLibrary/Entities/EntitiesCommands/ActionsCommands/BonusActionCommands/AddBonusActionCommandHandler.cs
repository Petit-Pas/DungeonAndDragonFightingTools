using System;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommandHandlers;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.BonusActionCommands;

public class AddBonusActionCommandHandler : BaseDndCommandHandler<AddBonusActionCommand, IMediatorCommandResponse>
{
    public override IMediatorCommandResponse Execute(AddBonusActionCommand command)
    {
        var target = command.GetEntity();

        if (!target.HasBonusAction)
        {
            target.HasBonusAction = true;
            command.CommandStatus = MediatorCommandStatii.Success;
        }
        else
        {
            Console.WriteLine($"WARN: trying to add action for {target.DisplayName} while he has one already");
            command.CommandStatus = MediatorCommandStatii.Canceled;
        }

        return command.CommandStatus;
    }

    public override void Undo(AddBonusActionCommand command)
    {
        if (command.CommandStatus == MediatorCommandStatii.Success)
        {
            command.GetEntity().HasBonusAction = false;
        }
    }
}