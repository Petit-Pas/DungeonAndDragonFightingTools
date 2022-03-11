using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using BaseToolsLibrary.Mediator;
using BaseToolsLibrary.Mediator.CommandStatii;
using DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.LooseHp;
using DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.LooseTempHp;
using System;

namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.TakeDamage
{
    public class TakeDamageHandler : SuperCommandHandlerBase<TakeDamageCommand, IMediatorCommandResponse>
    {
        private static Lazy<ICustomConsole> console = new Lazy<ICustomConsole>(() => DIContainer.GetImplementation<ICustomConsole>());
        private static Lazy<IFontWeightProvider> fontWeightProvider = new Lazy<IFontWeightProvider>(() => DIContainer.GetImplementation<IFontWeightProvider>());

        public override IMediatorCommandResponse Execute(IMediatorCommand command)
        {
            TakeDamageCommand _command = base.castCommand(command);

            if (_command.Amount == 0)
                return MediatorCommandStatii.NoResponse;

            PlayableEntity target = _command.GetEntity();
            int remaining = _command.Amount;
            var startingHPs = target.HpString;

            console.Value.AddEntry($"{remaining}", fontWeightProvider.Value.Bold);
            console.Value.AddEntry(" total damages: ", fontWeightProvider.Value.Normal);
            console.Value.AddEntry($"{startingHPs}", fontWeightProvider.Value.Bold);
            var entryHash = console.Value.AddEntry(" => ", fontWeightProvider.Value.Bold);

            if (target.TempHp != 0)
                remaining = handleTempHp(_command, target, remaining);
            handleHp(_command, target, remaining);

            console.Value.AddEntryAfter(entryHash, $"{target.HpString}\r\n", fontWeightProvider.Value.Bold);

            return MediatorCommandStatii.NoResponse;
        }

        private void handleHp(TakeDamageCommand command, PlayableEntity target, int remaining)
        {
            LooseHpCommand inner_command = new LooseHpCommand(target, remaining);

            _mediator.Value.Execute(inner_command);
            command.PushToInnerCommands(inner_command);
        }

        private int handleTempHp(TakeDamageCommand command, PlayableEntity target, int remaining)
        {
            int amount = remaining < target.TempHp ? remaining : target.TempHp;

            LooseTempHpCommand inner_command = new LooseTempHpCommand(target, amount);

            _mediator.Value.Execute(inner_command);
            command.PushToInnerCommands(inner_command);

            return remaining - amount;

        }
    }
}
