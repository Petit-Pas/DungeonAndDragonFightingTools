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

            command.AddLog($"{remaining}", FontWeightProvider.Bold);
            command.AddLog(" total damages: ", FontWeightProvider.Normal);
            command.AddLog($"{startingHPs}", FontWeightProvider.Bold);
            var entryHash = command.AddLog(" => ", FontWeightProvider.Bold);
            command.AddLog("\r\n");

            if (target.TempHp != 0)
                remaining = HandleTempHp(command, target, remaining);
            HandleHp(command, target, remaining);

            command.AddLogAfter(entryHash, $"{target.HpString}", FontWeightProvider.Bold);

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
