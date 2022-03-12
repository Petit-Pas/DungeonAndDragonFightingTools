using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using BaseToolsLibrary.Mediator;
using BaseToolsLibrary.Mediator.CommandStatii;
using System;

namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.TempHeal
{
    public class TempHealHandler : BaseMediatorHandler<TempHealCommand, IMediatorCommandResponse>
    {
        private static Lazy<ICustomConsole> console = new Lazy<ICustomConsole>(() => DIContainer.GetImplementation<ICustomConsole>());
        private static Lazy<IFontWeightProvider> fontWeightProvider = new Lazy<IFontWeightProvider>(() => DIContainer.GetImplementation<IFontWeightProvider>());

        public override IMediatorCommandResponse Execute(IMediatorCommand genericCommand)
        {
            TempHealCommand _command = this.castCommand(genericCommand);
            PlayableEntity target = _command.GetEntity();

            console.Value.AddEntry($"{target.DisplayName} regains {_command.Amount} temporary HPs.\r\n", fontWeightProvider.Value.Bold);

            _command.From = target.TempHp;

            if (_command.Amount > target.TempHp)
                target.TempHp = _command.Amount;

            _command.To = target.TempHp;
            return MediatorCommandStatii.NoResponse;
        }

        public override void Undo(IMediatorCommand genericCommand)
        {
            TempHealCommand _command = this.castCommand(genericCommand);

            if (false == _command.To.HasValue || false == _command.From.HasValue)
            {
                Console.WriteLine($"ERROR : Trying to undo a {this.GetType()} genericCommand that was not executed first");
                throw new NullReferenceException($"Trying to undo a {this.GetType()} genericCommand that was not executed first");
            }

            PlayableEntity target = _command.GetEntity();
            target.TempHp = _command.From.Value;
        }
    }
}
