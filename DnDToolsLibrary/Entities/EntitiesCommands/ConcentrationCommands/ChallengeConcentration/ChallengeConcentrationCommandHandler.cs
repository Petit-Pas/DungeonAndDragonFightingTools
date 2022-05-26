using System.Resources;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommands;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.ConcentrationCheckQueries;
using DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.LoseConcentration;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.ChallengeConcentration
{
    public class ChallengeConcentrationCommandHandler : SuperDndCommandHandlerBase<ChallengeConcentrationCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(ChallengeConcentrationCommand command)
        {
            var entity = command.GetEntity();

            if (!entity.IsFocused)
            {
                return MediatorCommandStatii.Canceled;
            }

            var saving = GetConcentrationCheckResult(command);

            if (saving == null)
            {
                // clearing messages when command was canceled
                FightConsole.RemoveEntries(command.LogMessages);
                return MediatorCommandStatii.Canceled;
            }
            
            if (saving.IsFailed)
            {
                CancelConcentration(command);
            }
            else
            {
                KeepConcentration(command);
            }

            return MediatorCommandStatii.Success;
        }

        private void KeepConcentration(ChallengeConcentrationCommand command)
        {
            command.LogMessages.Add(
                FightConsole.AddEntry("success.\r\n", FontWeightProvider.Bold));
        }

        private void CancelConcentration(ChallengeConcentrationCommand command)
        {
            command.LogMessages.Add(FightConsole
                .AddEntry("failed", FontWeightProvider.Bold));
            command.LogMessages.Add(FightConsole
                .AddEntry(", concentration lost.\r\n"));

            var loseConcentrationCommand = new LoseConcentrationCommand(command.GetEntityName());
            command.InnerCommands.Push(loseConcentrationCommand);
            Mediator.Execute(loseConcentrationCommand);
        }

        private static SavingThrow? GetConcentrationCheckResult(ChallengeConcentrationCommand command)
        {
            command.LogMessages.Add(FightConsole
                .AddEntry(command.GetEntityName(), FontWeightProvider.Bold));
            command.LogMessages.Add(FightConsole
                .AddEntry(" has taken damage and needs to pass a concentration check: "));

            var concentrationCheckQuery = new ConcentrationCheckQuery(command.GetEntityName());
            var validableSaving = Mediator.Execute(concentrationCheckQuery) as ValidableResponse<SavingThrow>;

            if (validableSaving.IsValid)
                return validableSaving.Response;
            return null;
        }
    }
}
