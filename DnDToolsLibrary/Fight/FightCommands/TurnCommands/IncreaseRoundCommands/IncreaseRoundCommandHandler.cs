using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommandHandlers;

namespace DnDToolsLibrary.Fight.FightCommands.TurnCommands.IncreaseRoundCommands
{
    public class IncreaseRoundCommandHandler : BaseDndCommandHandler<IncreaseRoundCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(IncreaseRoundCommand genericCommand)
        {
            if (TurnManager.TurnIndex != FightersProvider.FighterCount)
            {
                return MediatorCommandStatii.Canceled;
            }
            TurnManager.RoundCount += 1;
            TurnManager.TurnIndex = 0;
            return MediatorCommandStatii.Success;
        }

        public override void Undo(IncreaseRoundCommand genericCommand)
        {
            TurnManager.TurnIndex = FightersProvider.FighterCount;
            TurnManager.RoundCount -= 1;
        }
    }
}
