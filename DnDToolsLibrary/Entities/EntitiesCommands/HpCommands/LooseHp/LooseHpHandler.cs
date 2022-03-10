using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using BaseToolsLibrary.Mediator;
using BaseToolsLibrary.Mediator.CommandStatii;
using DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.ChallengeConcentration;
using System;

namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.LooseHp
{
    public class LooseHpHandler : SuperCommandHandlerBase<LooseHpCommand, IMediatorCommandResponse>
    {
        private static Lazy<ICustomConsole> console = new Lazy<ICustomConsole>(() => DIContainer.GetImplementation<ICustomConsole>());
        private static Lazy<IFontWeightProvider> fontWeightProvider = new Lazy<IFontWeightProvider>(() => DIContainer.GetImplementation<IFontWeightProvider>());

        public override IMediatorCommandResponse Execute(IMediatorCommand command)
        {
            LooseHpCommand _command = this.castCommand(command);
            PlayableEntity target = _command.GetEntity();

            _command.From = target.Hp;

            target.Hp -= _command.Amount;
            if (target.Hp < 0)
                target.Hp = 0;

            if (target.IsFocused && (target.Hp <= 0 || _command.Amount > 0))
            {
                ChallengeConcentrationCommand concentrationCommand = new ChallengeConcentrationCommand(target.DisplayName);
                base._mediator.Value.Execute(concentrationCommand);
                _command.PushToInnerCommands(concentrationCommand);
            }

            _command.To = target.Hp;
            return MediatorCommandStatii.NoResponse;
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
