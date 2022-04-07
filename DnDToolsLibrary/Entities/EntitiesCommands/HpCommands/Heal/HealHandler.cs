using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using BaseToolsLibrary.Mediator;
using System;

namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.Heal
{
    public class HealHandler : BaseMediatorHandler<HealCommand, IMediatorCommandResponse>
    {
        private static Lazy<ICustomConsole> console = new Lazy<ICustomConsole>(DIContainer.GetImplementation<ICustomConsole>);
        private static Lazy<IFontWeightProvider> fontWeightProvider = new Lazy<IFontWeightProvider>(DIContainer.GetImplementation<IFontWeightProvider>);

        public override IMediatorCommandResponse Execute(HealCommand command)
        {
            PlayableEntity target = command.GetEntity();

            if (command.Amount < 0)
            {
                Console.WriteLine($"WARNING : Trying to heal {target.DisplayName} for {command.Amount}, will be set to 0 instead");
                command.Amount = 0;
            }

            console.Value.AddEntry($"{target.DisplayName} regains {command.Amount} HPs.\r\n", fontWeightProvider.Value.Bold);

            command.From = target.Hp;

            if (target.Hp + command.Amount > target.MaxHp)
                target.Hp = (int)target.MaxHp;
            else
                target.Hp += command.Amount;

            command.To = target.Hp;
            return MediatorCommandStatii.NoResponse;
        }

        public override void Undo(HealCommand command)
        {
            if (false == command.To.HasValue || false == command.From.HasValue)
            {
                Console.WriteLine($"ERROR : Trying to undo a {this.GetType()} genericCommand that was not executed first");
                throw new NullReferenceException($"Trying to undo a {this.GetType()} genericCommand that was not executed first");
            }
            
            PlayableEntity target = command.GetEntity();
            target.Hp = command.From.Value;
        }
    }
}
