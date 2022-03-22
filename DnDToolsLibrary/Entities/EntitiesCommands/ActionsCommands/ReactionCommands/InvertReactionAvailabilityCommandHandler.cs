using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ReactionCommands;

public class InvertReactionAvailabilityCommandHandler : SuperCommandHandlerBase<InvertReactionAvailabilityCommand, IMediatorCommandResponse>
{
    public override IMediatorCommandResponse Execute(IMediatorCommand genericCommand)
    {
        IMediatorCommand innerCommand;
        var command = base.castCommand(genericCommand);
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
        _mediator.Value.Execute(innerCommand);

        return MediatorCommandStatii.NoResponse;
    }
}