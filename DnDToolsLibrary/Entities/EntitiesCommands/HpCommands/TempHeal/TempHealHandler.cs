﻿using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using BaseToolsLibrary.Mediator;
using System;

namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.TempHeal
{
    public class TempHealHandler : BaseMediatorHandler<TempHealCommand, NoResponse>
    {
        private static Lazy<ICustomConsole> console = new Lazy<ICustomConsole>(() => DIContainer.GetImplementation<ICustomConsole>());
        private static Lazy<IFontWeightProvider> fontWeightProvider = new Lazy<IFontWeightProvider>(() => DIContainer.GetImplementation<IFontWeightProvider>());
        private static Lazy<IFontColorProvider> colorProvider = new Lazy<IFontColorProvider>(() => DIContainer.GetImplementation<IFontColorProvider>());

        public override NoResponse Execute(IMediatorCommand command)
        {
            TempHealCommand _command = this.cast_command(command);
            PlayableEntity target = _command.GetEntity();

            console.Value.AddEntry($"{target.DisplayName} regains {_command.Amount} temporary HPs.\r\n", fontWeightProvider.Value.Bold);

            _command.From = target.TempHp;

            if (_command.Amount > target.TempHp)
                target.TempHp = _command.Amount;

            _command.To = target.TempHp;
            return MediatorCommandResponses.NoResponse;
        }

        public override void Undo(IMediatorCommand command)
        {
            TempHealCommand _command = this.cast_command(command);

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
