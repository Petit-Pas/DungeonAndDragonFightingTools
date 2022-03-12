using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ActionCommands;

public class RemoveActionCommand : EntityCommand
{
    public RemoveActionCommand(string entityName) : base(entityName)
    {
    }

    public IMediatorCommandResponse CommandStatus { get; set; }
}