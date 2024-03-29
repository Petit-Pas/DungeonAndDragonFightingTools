﻿using System.Resources;
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
                return command.Cancel();
            }

            var saving = GetConcentrationCheckResult(command);

            if (saving == null)
            {
                // clearing messages when command was canceled
                return command.Cancel();
            }

            command.AddLog("Saving: ");
            command.AddLog($"{saving.Result}/{saving.Difficulty} => ", FontWeightProvider.Bold);
            command.AddLog($"{(saving.IsSuccesful ? "succeeded" : "failed")}\r\n", FontWeightProvider.SemiBold,
                saving.IsSuccesful ?
                    FontColorProvider.Success :
                    FontColorProvider.Failure);

            if (saving.IsFailed)
            {
                CancelConcentration(command);
            }
            
            return MediatorCommandStatii.Success;
        }

        private void CancelConcentration(ChallengeConcentrationCommand command)
        {
            var loseConcentrationCommand = new LoseConcentrationCommand(command.GetEntityName());
            command.InnerCommands.Push(loseConcentrationCommand);
            Mediator.Execute(loseConcentrationCommand);
        }

        private static SavingThrow? GetConcentrationCheckResult(ChallengeConcentrationCommand command)
        {
            command.AddLog(command.GetEntityName(), FontWeightProvider.Bold);
            command.AddLog(" has taken damage and needs to pass a concentration check: ");

            var concentrationCheckQuery = new ConcentrationCheckQuery(command.GetEntityName());
            var validableSaving = Mediator.Execute(concentrationCheckQuery) as ValidableResponse<SavingThrow>;

            if (validableSaving.IsValid)
                return validableSaving.Response;
            return null;
        }
    }
}
