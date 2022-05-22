﻿using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommandHandlers;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ReactionCommands;

public class InvertReactionAvailabilityCommandHandler : SuperDndCommandHandler<InvertReactionAvailabilityCommand, IMediatorCommandResponse>
{
    public override IMediatorCommandResponse Execute(InvertReactionAvailabilityCommand command)
    {
        IMediatorCommand innerCommand;
        var target = command.GetEntity();

        if (target.HasReaction)
        {
            innerCommand = new RemoveReactionCommand(target.DisplayName);
        }
        else
        {
            innerCommand = new AddReactionCommand(target.DisplayName);
        }

        command.PushToInnerCommands(innerCommand);
        Mediator.Execute(innerCommand);

        return MediatorCommandStatii.NoResponse;
    }
}