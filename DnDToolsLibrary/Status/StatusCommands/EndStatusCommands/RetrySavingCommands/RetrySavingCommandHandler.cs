using System;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Dice.DiceCommancs.SavingThrowCommands.SavingThrowQueries;
using DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.RemoveStatus;
using DnDToolsLibrary.Fight;

namespace DnDToolsLibrary.Status.StatusCommands.EndStatusCommands.RetrySavingCommands
{
    internal class RetrySavingCommandHandler : SuperCommandHandlerBase<RetrySavingCommand, IMediatorCommandResponse>
    {
        private static readonly Lazy<IStatusProvider> _lazyStatusProvider = new Lazy<IStatusProvider>(DIContainer.GetImplementation<IStatusProvider>);
        private static IStatusProvider _statusProvider => _lazyStatusProvider.Value;

        public override IMediatorCommandResponse Execute(IMediatorCommand genericCommand)
        {
            var command = base.castCommand(genericCommand);
            var status = _statusProvider.GetOnHitStatusById(command.StatusId);

            if (status != null)
            {
                var saving = status.GetSavingThrow(status.Caster, status.Target);
                var savingThrowQuery = new SavingThrowQuery(saving, $"{status.TargetName} attempts to end the {status.DisplayName} status...");
                var savingResult = base._mediator.Value.Execute(savingThrowQuery) as ValidableResponse<SavingThrow>;

                if (savingResult?.IsValid == false)
                {
                    return MediatorCommandStatii.Canceled;
                }

                if (savingResult?.Response?.IsSuccesful == true)
                {
                    var removeStatusCommand = new RemoveStatusCommand(status.Id, status.TargetName);

                    command.PushToInnerCommands(removeStatusCommand);
                    base._mediator.Value.Execute(removeStatusCommand);
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
