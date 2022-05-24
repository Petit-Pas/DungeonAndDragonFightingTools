using System;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ReactionCommands;

public class AddReactionCommandHandler : DndCommandHandlerBase<AddReactionCommand, IMediatorCommandResponse>
{
    public override IMediatorCommandResponse Execute(AddReactionCommand command)
    {
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

    public override void Undo(AddReactionCommand command)
    {
        if (command.CommandStatus == MediatorCommandStatii.Success)
        {
            command.GetEntity().HasReaction = false;
        }
    }
}