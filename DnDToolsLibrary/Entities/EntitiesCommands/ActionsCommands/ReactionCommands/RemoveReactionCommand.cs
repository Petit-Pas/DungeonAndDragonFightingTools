using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ReactionCommands;

public class RemoveReactionCommand : EntityCommand
{
    public RemoveReactionCommand(string entityName) : base(entityName)
    {
    }

    public IMediatorCommandResponse CommandStatus { get; set; }
}