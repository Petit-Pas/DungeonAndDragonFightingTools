using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Entities.EntitiesCommands.TurnCommands.EndTurnCommands
{
    public class EndTurnCommandHandler : SuperCommandHandlerBase<EndTurnCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(EndTurnCommand genericCommand)
        {
            throw new System.NotImplementedException();
        }
    }
}
