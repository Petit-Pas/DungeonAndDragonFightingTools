using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ReactionCommands;

public class ResetReactionAvailabilityCommandHandler : SuperCommandHandlerBase<ResetReactionAvailabilityCommand, IMediatorCommandResponse>
{
    public override IMediatorCommandResponse Execute(ResetReactionAvailabilityCommand command)
    {
        var target = command.GetEntity();

        if (!target.HasReaction)
        {
            var innerCommand = new AddReactionCommand(target.DisplayName);
            command.PushToInnerCommands(innerCommand);
            _mediator.Value.Execute(innerCommand);
            return MediatorCommandStatii.Success;
        }
        return MediatorCommandStatii.Canceled;
    }
}