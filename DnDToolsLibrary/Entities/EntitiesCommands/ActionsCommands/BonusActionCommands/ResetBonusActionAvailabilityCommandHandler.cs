using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommandHandlers;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.BonusActionCommands;

public class ResetBonusActionAvailabilityCommandHandler : SuperDndCommandHandler<ResetBonusActionAvailabilityCommand, IMediatorCommandResponse>
{
    public override IMediatorCommandResponse Execute(ResetBonusActionAvailabilityCommand command)
    {
        var target = command.GetEntity();

        if (!target.HasBonusAction)
        {
            var innerCommand = new AddBonusActionCommand(target.DisplayName);
            command.PushToInnerCommands(innerCommand);
            Mediator.Execute(innerCommand);
            return MediatorCommandStatii.Success;
        }
        return MediatorCommandStatii.Canceled;
    }
}