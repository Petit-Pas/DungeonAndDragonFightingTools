using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.LooseHp;
using DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.LooseTempHp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.TakeDamage
{
    public class TakeDamageHandler : BaseSuperHandler<TakeDamageCommand, NoResponse>
    {
        private static Lazy<ICustomConsole> console = new Lazy<ICustomConsole>(() => DIContainer.GetImplementation<ICustomConsole>());
        private static Lazy<IFontWeightProvider> fontWeightProvider = new Lazy<IFontWeightProvider>(() => DIContainer.GetImplementation<IFontWeightProvider>());
        private static Lazy<IFontColorProvider> colorProvider = new Lazy<IFontColorProvider>(() => DIContainer.GetImplementation<IFontColorProvider>());

        public override NoResponse Execute(IMediatorCommand command)
        {
            TakeDamageCommand _command = base.cast_command(command);

            if (_command.Amount == 0)
                return MediatorCommandResponses.NoResponse;

            PlayableEntity target = _command.GetEntity();
            int remaining = _command.Amount;

            console.Value.AddEntry($"Total: {remaining} Damages\r\n", fontWeightProvider.Value.Bold);

            if (target.TempHp != 0)
                remaining = handleTempHp(_command, target, remaining);
            handleHp(_command, target, remaining);
            return MediatorCommandResponses.NoResponse;
        }

        private void handleHp(TakeDamageCommand command, PlayableEntity target, int remaining)
        {
            LooseHpCommand inner_command = new LooseHpCommand(target, remaining);

            _mediator.Value.Execute(inner_command);
            command.AddToInnerCommands(inner_command);
        }

        private int handleTempHp(TakeDamageCommand command, PlayableEntity target, int remaining)
        {
            int amount = remaining < target.TempHp ? remaining : target.TempHp;

            LooseTempHpCommand inner_command = new LooseTempHpCommand(target, amount);

            _mediator.Value.Execute(inner_command);
            command.AddToInnerCommands(inner_command);

            return remaining - amount;

        }
    }
}
