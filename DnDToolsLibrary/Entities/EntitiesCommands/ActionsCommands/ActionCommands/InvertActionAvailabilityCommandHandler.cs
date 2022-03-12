using System;
using System.Collections.Generic;
using System.Text;
using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ActionCommands;

public class InvertActionAvailabilityCommandHandler : SuperCommandHandlerBase<InvertActionAvailabilityCommand, IMediatorCommandResponse>
{
    public override IMediatorCommandResponse Execute(IMediatorCommand genericCommand)
    {
        IMediatorCommand innerCommand;
        var command = base.castCommand(genericCommand);
        var target = command.GetEntity();

        if (target.HasAction)
        {
            innerCommand = new RemoveActionCommand(target.DisplayName);
        }
        else
        {
            innerCommand = new AddActionCommand(target.DisplayName);
        }

        command.PushToInnerCommands(innerCommand);
        _mediator.Value.Execute(innerCommand);

        return MediatorCommandStatii.NoResponse;
    }
}