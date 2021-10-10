using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using BaseToolsLibrary.Mediator;
using System;

namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.LooseHp
{
    public class LooseHpHandler : BaseMediatorHandler<LooseHpCommand, NoResponse>
    {
        private static Lazy<ICustomConsole> console = new Lazy<ICustomConsole>(() => DIContainer.GetImplementation<ICustomConsole>());
        private static Lazy<IFontWeightProvider> fontWeightProvider = new Lazy<IFontWeightProvider>(() => DIContainer.GetImplementation<IFontWeightProvider>());

        public override NoResponse Execute(IMediatorCommand command)
        {
            LooseHpCommand _command = this.castCommand(command);
            PlayableEntity target = _command.GetEntity();

            console.Value.AddEntry($"{target.DisplayName} looses {_command.Amount} HPs.\r\n", fontWeightProvider.Value.Bold);

            _command.From = target.Hp;

            target.Hp -= _command.Amount;
            if (target.Hp < 0)
                target.Hp = 0;

            _command.To = target.Hp;
            return MediatorCommandResponses.NoResponse;
        }

        public override void Undo(IMediatorCommand command)
        {
            LooseHpCommand _command = this.castCommand(command);

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
