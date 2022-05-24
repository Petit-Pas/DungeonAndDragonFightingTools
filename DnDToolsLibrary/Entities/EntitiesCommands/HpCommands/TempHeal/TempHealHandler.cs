using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using BaseToolsLibrary.Mediator;
using System;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.TempHeal
{
    public class TempHealHandler : DndCommandHandlerBase<TempHealCommand, IMediatorCommandResponse>
    {
        private static Lazy<ICustomConsole> console = new Lazy<ICustomConsole>(() => DIContainer.GetImplementation<ICustomConsole>());
        private static Lazy<IFontWeightProvider> fontWeightProvider = new Lazy<IFontWeightProvider>(() => DIContainer.GetImplementation<IFontWeightProvider>());

        public override IMediatorCommandResponse Execute(TempHealCommand command)
        {
            PlayableEntity target = command.GetEntity();

            console.Value.AddEntry($"{target.DisplayName} regains {command.Amount} temporary HPs.\r\n", fontWeightProvider.Value.Bold);

            command.From = target.TempHp;

            if (command.Amount > target.TempHp)
                target.TempHp = command.Amount;

            command.To = target.TempHp;
            return MediatorCommandStatii.NoResponse;
        }

        public override void Undo(TempHealCommand command)
        {
            if (false == command.To.HasValue || false == command.From.HasValue)
            {
                Console.WriteLine($"ERROR : Trying to undo a {this.GetType()} genericCommand that was not executed first");
                throw new NullReferenceException($"Trying to undo a {this.GetType()} genericCommand that was not executed first");
            }

            PlayableEntity target = command.GetEntity();
            target.TempHp = command.From.Value;
        }
    }
}
