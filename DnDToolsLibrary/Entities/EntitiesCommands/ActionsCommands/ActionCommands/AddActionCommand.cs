using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ActionCommands;

public class AddActionCommand : EntityCommand
{
    public AddActionCommand(string entityName) : base(entityName)
    {
    }

    public IMediatorCommandResponse CommandStatus { get; set; }
}