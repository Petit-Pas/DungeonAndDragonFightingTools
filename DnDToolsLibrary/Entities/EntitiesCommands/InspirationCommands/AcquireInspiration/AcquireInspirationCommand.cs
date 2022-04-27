using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Entities.EntitiesCommands.InspirationCommands.AcquireInspiration
{
    public class AcquireInspirationCommand : EntityCommand
    {
        public AcquireInspirationCommand(string entityName) : base(entityName)
        {
        }

        public IMediatorCommandResponse Status { get; set; }
    }
}
