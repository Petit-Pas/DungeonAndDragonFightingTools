using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using BaseToolsLibrary.Mediator;
using System;

namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.Heal
{
    public class HealHandler : BaseMediatorHandler<HealCommand, NoResponse>
    {
        private static Lazy<ICustomConsole> console = new Lazy<ICustomConsole>(() => DIContainer.GetImplementation<ICustomConsole>());
        private static Lazy<IFontWeightProvider> fontWeightProvider = new Lazy<IFontWeightProvider>(() => DIContainer.GetImplementation<IFontWeightProvider>());

        public override NoResponse Execute(IMediatorCommand command)
        {
            HealCommand _command = this.castCommand(command);
            PlayableEntity target = _command.GetEntity();

            if (_command.Amount < 0)
            {
                Console.WriteLine($"WARNING : Trying to heal {target.DisplayName} for {_command.Amount}, will be set to 0 instead");
                _command.Amount = 0;
            }

            console.Value.AddEntry($"{target.DisplayName} regains {_command.Amount} HPs.\r\n", fontWeightProvider.Value.Bold);

            _command.From = target.Hp;

            if (target.Hp + _command.Amount > target.MaxHp)
                target.Hp = (int)target.MaxHp;
            else
                target.Hp += _command.Amount;

            _command.To = target.Hp;
            return MediatorCommandResponses.NoResponse;
        }

        public override void Undo(IMediatorCommand command)
        {
            HealCommand _command = this.castCommand(command);
            
            if (false == _command.To.HasValue || false == _command.From.HasValue)
            {
                Console.WriteLine($"ERROR : Trying to undo a {this.GetType()} command that was not executed first");
                throw new NullReferenceException($"Trying to undo a {this.GetType()} command that was not executed first");
            }
            
            PlayableEntity target = _command.GetEntity();
            target.Hp = _command.From.Value;
        }
    }
}
