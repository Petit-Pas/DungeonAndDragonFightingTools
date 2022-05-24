using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ReactionCommands;

public class ResetReactionAvailabilityCommandHandler : SuperDndCommandHandlerBase<ResetReactionAvailabilityCommand, IMediatorCommandResponse>
{
    public override IMediatorCommandResponse Execute(ResetReactionAvailabilityCommand command)
    {
        var target = command.GetEntity();

        if (!target.HasReaction)
        {
            var innerCommand = new AddReactionCommand(target.DisplayName);
            command.PushToInnerCommands(innerCommand);
            Mediator.Execute(innerCommand);
            return MediatorCommandStatii.Success;
        }
        return MediatorCommandStatii.Canceled;
    }
}