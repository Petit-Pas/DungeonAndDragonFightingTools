using System;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ActionCommands;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ReactionCommands;

public class AddReactionCommandHandler : BaseMediatorHandler<AddReactionCommand, IMediatorCommandResponse>
{
    public override IMediatorCommandResponse Execute(IMediatorCommand genericCommand)
    {
        var command = base.castCommand(genericCommand);
        var target = command.GetEntity();

        if (!target.HasReaction)
        {
            target.HasReaction = true;
            command.CommandStatus = MediatorCommandStatii.Success;
        }
        else
        {
            Console.WriteLine($"WARN: trying to add action for {target.DisplayName} while he has one already");
            command.CommandStatus = MediatorCommandStatii.Canceled;
        }

        return command.CommandStatus;
    }

    public override void Undo(IMediatorCommand genericCommand)
    {
        var command = base.castCommand(genericCommand);

        if (command.CommandStatus == MediatorCommandStatii.Success)
        {
            command.GetEntity().HasReaction = false;
        }
    }
}