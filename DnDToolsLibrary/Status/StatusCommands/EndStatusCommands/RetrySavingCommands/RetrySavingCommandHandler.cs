using System;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommands;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Dice.DiceCommancs.SavingThrowCommands.SavingThrowQueries;
using DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.RemoveStatus;

namespace DnDToolsLibrary.Status.StatusCommands.EndStatusCommands.RetrySavingCommands
{
    internal class RetrySavingCommandHandler : SuperDndCommandHandlerBase<RetrySavingCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(RetrySavingCommand command)
        {
            var status = StatusProvider.GetOnHitStatusById(command.StatusId);

            if (status != null)
            {
                var saving = status.GetSavingThrow(status.Caster, status.Target);
                var savingThrowQuery = new SavingThrowQuery(saving, $"{status.TargetName} attempts to end the {status.DisplayName} status...");
                var savingResult = Mediator.Execute(savingThrowQuery) as ValidableResponse<SavingThrow>;

                if (savingResult?.IsValid == false)
                {
                    return MediatorCommandStatii.Canceled;
                }

                if (savingResult?.Response?.IsSuccesful == true)
                {
                    var removeStatusCommand = new RemoveStatusCommand(status.Id, status.TargetName);

                    command.PushToInnerCommands(removeStatusCommand);
                    Mediator.Execute(removeStatusCommand);
                    return MediatorCommandStatii.Success;
                }
                else
                {
                    return MediatorCommandStatii.Failed;
                }
            }

            Console.WriteLine("Debug: a retry saving command has been executed on something that isn't an OnHitStatus");
            return MediatorCommandStatii.Canceled;
        }
    }
}
