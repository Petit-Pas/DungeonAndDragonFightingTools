using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Fight.FightCommands.TurnCommands.IncreaseRoundCommands
{
    public class IncreaseRoundCommandHandler : DndCommandHandlerBase<IncreaseRoundCommand, IMediatorCommandResponse>
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

        public override void Undo(IncreaseRoundCommand command)
        {
            base.Undo(command);

            TurnManager.TurnIndex = FightersProvider.FighterCount;
            TurnManager.RoundCount -= 1;
        }
    }
}
