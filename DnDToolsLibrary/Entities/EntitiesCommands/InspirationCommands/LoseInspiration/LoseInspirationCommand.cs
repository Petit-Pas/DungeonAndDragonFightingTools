using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Entities.EntitiesCommands.InspirationCommands.LoseInspiration
{
    public class LoseInspirationCommand : EntityCommand
    {
        public LoseInspirationCommand(string entityName) : base(entityName)
        {
        }

        public IMediatorCommandResponse Status { get; set; }
    }
}
