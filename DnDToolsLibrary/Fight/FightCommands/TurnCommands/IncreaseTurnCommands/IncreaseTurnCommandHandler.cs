using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommandHandlers;
using DnDToolsLibrary.Fight.FightCommands.TurnCommands.IncreaseRoundCommands;

namespace DnDToolsLibrary.Fight.FightCommands.TurnCommands.IncreaseTurnCommands
{
    public class IncreaseTurnCommandHandler : SuperDndCommandHandler<IncreaseTurnCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(IncreaseTurnCommand increaseTurnCommand)
        {
            TurnManager.TurnIndex += 1;
            if (TurnManager.TurnIndex == FightersProvider.FighterCount)
            {
                var increaseRoundCommand = new IncreaseRoundCommand();
                Mediator.Execute(increaseRoundCommand);
                increaseTurnCommand.PushToInnerCommands(increaseRoundCommand);
            }

            return MediatorCommandStatii.NoResponse;
        }

        public override void Undo(IncreaseTurnCommand command)
        {
            base.Undo(command);
            TurnManager.TurnIndex -= 1;
        }
    }
}
