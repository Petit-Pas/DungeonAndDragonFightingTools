using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ActionCommands;
using DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.BonusActionCommands;

namespace DnDToolsLibrary.Entities.EntitiesCommands.TurnCommands.StartTurnCommands
{
    internal class StartTurnCommandHandler : SuperCommandHandlerBase<StartTurnCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(StartTurnCommand command)
        {
            ResetActions(command);

            return default;
        }

        private void ResetActions(StartTurnCommand command)
        {
            var actionCommand = new ResetActionAvailabilityCommand(command.GetEntityName());
            var bonusActionCommand = new ResetBonusActionAvailabilityCommand(command.GetEntityName());

        }

    }
}
