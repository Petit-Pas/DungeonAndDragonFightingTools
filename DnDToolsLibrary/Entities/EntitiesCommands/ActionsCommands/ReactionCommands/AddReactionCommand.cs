using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ReactionCommands;

public class AddReactionCommand : EntityCommand
{
    public AddReactionCommand(string entityName) : base(entityName)
    {
    }

    public IMediatorCommandResponse CommandStatus { get; set; }
}