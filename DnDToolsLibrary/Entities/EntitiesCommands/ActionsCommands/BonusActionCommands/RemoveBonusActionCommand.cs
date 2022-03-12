using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.BonusActionCommands;

public class RemoveBonusActionCommand : EntityCommand
{
    public RemoveBonusActionCommand(string entityName) : base(entityName)
    {
    }

    public IMediatorCommandResponse CommandStatus { get; set; }
}