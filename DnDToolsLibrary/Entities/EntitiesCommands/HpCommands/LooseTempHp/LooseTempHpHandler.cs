﻿using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using BaseToolsLibrary.Mediator;
using BaseToolsLibrary.Mediator.CommandStatii;
using System;

namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.LooseTempHp
{
    public class LooseTempHpHandler : BaseMediatorHandler<LooseTempHpCommand, MediatorCommandNoResponse>
    {
        private static Lazy<ICustomConsole> console = new Lazy<ICustomConsole>(() => DIContainer.GetImplementation<ICustomConsole>());
        private static Lazy<IFontWeightProvider> fontWeightProvider = new Lazy<IFontWeightProvider>(() => DIContainer.GetImplementation<IFontWeightProvider>());

        public override MediatorCommandNoResponse Execute(IMediatorCommand command)
        {
            LooseTempHpCommand _command = this.castCommand(command);
            PlayableEntity target = _command.GetEntity();

            console.Value.AddEntry($"{target.DisplayName} looses {_command.Amount} temporary HPs.\r\n", fontWeightProvider.Value.Bold);

            _command.From = target.TempHp;

            target.TempHp -= _command.Amount;
            if (target.TempHp < 0)
            {
                throw new InvalidOperationException($"WARNING : Trying to remove more TempHps than what {target.DisplayName} has. TempsHps will be set to 0");
            }

            _command.To = target.TempHp;
            return MediatorCommandStatii.NoResponse;
        }

        public override void Undo(IMediatorCommand command)
        {
            LooseTempHpCommand _command = this.castCommand(command);

            if (false == _command.To.HasValue || false == _command.From.HasValue)
            {
                Console.WriteLine($"ERROR : Trying to undo a {this.GetType()} command that was not executed first");
                throw new NullReferenceException($"Trying to undo a {this.GetType()} command that was not executed first");
            }

            PlayableEntity target = _command.GetEntity();
            target.TempHp = _command.From.Value;
        }
    }
}
