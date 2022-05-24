using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.BonusActionCommands;

public class InvertBonusActionAvailabilityCommandHandler : SuperDndCommandHandlerBase<InvertBonusActionAvailabilityCommand, IMediatorCommandResponse>
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
        Mediator.Execute(innerCommand);

        return MediatorCommandStatii.NoResponse;
    }
}