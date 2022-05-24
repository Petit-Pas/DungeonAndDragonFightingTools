using System;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.BonusActionCommands;

public class RemoveBonusActionCommandHandler : DndCommandHandlerBase<RemoveBonusActionCommand, IMediatorCommandResponse>
{
    public override IMediatorCommandResponse Execute(RemoveBonusActionCommand command)
    {
        var target = command.GetEntity();

        if (target.HasBonusAction)
        {
            target.HasBonusAction = false;
            command.CommandStatus = MediatorCommandStatii.Success;
        }
        else
        {
            Console.WriteLine($"WARN: trying to remove action for {target.DisplayName} while he has none");
            command.CommandStatus = MediatorCommandStatii.Canceled;
        }

        return command.CommandStatus;
    }

    public override void Undo(RemoveBonusActionCommand command)
    {
        if (command.CommandStatus == MediatorCommandStatii.Success)
        {
            command.GetEntity().HasBonusAction = true;
        }
    }
}