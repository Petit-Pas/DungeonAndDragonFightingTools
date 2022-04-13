﻿using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.BonusActionCommands;

public class InvertBonusActionAvailabilityCommandHandler : SuperCommandHandlerBase<InvertBonusActionAvailabilityCommand, IMediatorCommandResponse>
{
    public override IMediatorCommandResponse Execute(InvertBonusActionAvailabilityCommand command)
    {
        IMediatorCommand innerCommand;
        var target = command.GetEntity();

        if (target.HasBonusAction)
        {
            innerCommand = new RemoveBonusActionCommand(target.DisplayName);
        }
        else
        {
            innerCommand = new AddBonusActionCommand(target.DisplayName);
        }

        command.PushToInnerCommands(innerCommand);
        _mediator.Value.Execute(innerCommand);

        return MediatorCommandStatii.NoResponse;
    }
}