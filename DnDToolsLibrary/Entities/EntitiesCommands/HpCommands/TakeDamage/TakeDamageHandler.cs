using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.LooseHp;
using DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.LooseTempHp;
using System;
using System.Resources;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.TakeDamage
{
    public class TakeDamageHandler : SuperDndCommandHandlerBase<TakeDamageCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(TakeDamageCommand command)
        {
            if (command.Amount == 0)
                return MediatorCommandStatii.NoResponse;

            var target = command.GetEntity();
            var remaining = command.Amount;
            var startingHPs = target.HpString;

            command.LogMessages.Add(FightConsole
                .AddEntry($"{remaining}", FontWeightProvider.Bold));
            command.LogMessages.Add(FightConsole
                .AddEntry(" total damages: ", FontWeightProvider.Normal));
            command.LogMessages.Add(FightConsole
                .AddEntry($"{startingHPs}", FontWeightProvider.Bold));
            var entryHash = FightConsole
                .AddEntry(" => ", FontWeightProvider.Bold);
            command.LogMessages.Add(entryHash);
            command.LogMessages.Add(FightConsole
                .AddEntry("\r\n"));

            if (target.TempHp != 0)
                remaining = HandleTempHp(command, target, remaining);
            HandleHp(command, target, remaining);

            command.LogMessages.Add(FightConsole
                .AddEntryAfter(entryHash, $"{target.HpString}", FontWeightProvider.Bold));

            return MediatorCommandStatii.NoResponse;
        }

        private static void HandleHp(TakeDamageCommand command, PlayableEntity target, int remaining)
        {
            var looseHpCommand = new LooseHpCommand(target, remaining);

            Mediator.Execute(looseHpCommand);
            command.PushToInnerCommands(looseHpCommand);
        }

        private static int HandleTempHp(TakeDamageCommand command, PlayableEntity target, int remaining)
        {
            var amount = remaining < target.TempHp ? remaining : target.TempHp;

            var looseTempHpCommand = new LooseTempHpCommand(target, amount);

            Mediator.Execute(looseTempHpCommand);
            command.PushToInnerCommands(looseTempHpCommand);

            return remaining - amount;
        }
    }
}
