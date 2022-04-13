﻿using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.BonusActionCommands;

public class ResetBonusActionAvailabilityCommandHandler : SuperCommandHandlerBase<ResetBonusActionAvailabilityCommand, IMediatorCommandResponse>
{
    public override IMediatorCommandResponse Execute(ResetBonusActionAvailabilityCommand command)
    {
        var target = command.GetEntity();

        if (!target.HasBonusAction)
        {
            var innerCommand = new AddBonusActionCommand(target.DisplayName);
            command.PushToInnerCommands(innerCommand);
            _mediator.Value.Execute(innerCommand);
            return MediatorCommandStatii.Success;
        }
        return MediatorCommandStatii.Canceled;
    }
}