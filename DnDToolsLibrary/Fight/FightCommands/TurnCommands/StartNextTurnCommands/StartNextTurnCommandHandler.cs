using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommandHandlers;
using DnDToolsLibrary.Entities.EntitiesCommands.TurnCommands.EndTurnCommands;
using DnDToolsLibrary.Entities.EntitiesCommands.TurnCommands.StartTurnCommands;
using DnDToolsLibrary.Fight.FightCommands.TurnCommands.IncreaseTurnCommands;

namespace DnDToolsLibrary.Fight.FightCommands.TurnCommands.StartNextTurnCommands
{
    public class StartNextTurnCommandHandler : SuperDndCommandHandler<StartNextTurnCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(StartNextTurnCommand startNextTurnCommand)
        {
            EndTurn(startNextTurnCommand);

            IncreaseTurn(startNextTurnCommand);

            StartTurn(startNextTurnCommand);

            return MediatorCommandStatii.NoResponse;
        }

        private void EndTurn(StartNextTurnCommand command)
        {
            // can only end turn if this is not the 1st turn of the fight
            if (TurnManager.TurnIndex != -1)
            {
                var endingFighter = FightersProvider.GetFighterByIndex(TurnManager.TurnIndex);
                var endTurnCommand = new EndTurnCommand(endingFighter.DisplayName);
                Mediator.Execute(endTurnCommand);
                command.PushToInnerCommands(endTurnCommand);
            }
        }

        private void IncreaseTurn(StartNextTurnCommand startNextTurnCommand)
        {
            var increaseTurnCommand = new IncreaseTurnCommand();
            Mediator.Execute(increaseTurnCommand);
            startNextTurnCommand.PushToInnerCommands(increaseTurnCommand);
        }

        private void StartTurn(StartNextTurnCommand command)
        {
            var startingFighter = FightersProvider.GetFighterByIndex(TurnManager.TurnIndex);
            var startTurnCommand = new StartTurnCommand(startingFighter.DisplayName);
            Mediator.Execute(startTurnCommand);
            command.PushToInnerCommands(startTurnCommand);
        }
    }
}
