﻿using System;
using System.Linq;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;

namespace DnDToolsLibrary.BaseCommands
{
    public abstract class SuperDndCommandHandlerBase<TCommand, TResponse> : SuperCommandHandlerBase<TCommand, TResponse>
        where TCommand : SuperDndCommandBase
        where TResponse : class, IMediatorCommandResponse
    {
        private static readonly Lazy<ICustomConsole> _console = new(DIContainer.GetImplementation<ICustomConsole>);
        protected static ICustomConsole FightConsole => _console.Value;

        private static readonly Lazy<IFontWeightProvider> _fontWeightProvider = new(DIContainer.GetImplementation<IFontWeightProvider>);
        protected static IFontWeightProvider FontWeightProvider => _fontWeightProvider.Value;

        private static readonly Lazy<IFontColorProvider> _fontColorProvider = new(DIContainer.GetImplementation<IFontColorProvider>);
        protected static IFontColorProvider FontColorProvider => _fontColorProvider.Value;

        private static readonly Lazy<ITurnManager> _turnManager = new(DIContainer.GetImplementation<ITurnManager>);
        protected static ITurnManager TurnManager = _turnManager.Value;

        private static readonly Lazy<IFightersProvider> _fightersProvider = new(DIContainer.GetImplementation<IFightersProvider>);
        protected static IFightersProvider FightersProvider => _fightersProvider.Value;

        private static readonly Lazy<IStatusProvider> _statusProvider = new(DIContainer.GetImplementation<IStatusProvider>());
        protected static IStatusProvider StatusProvider => _statusProvider.Value;

        public override void Undo(TCommand command)
        {
            base.Undo(command);
            FightConsole.RemoveEntries(command.GetLogHashes());
            command.ClearLogs();
        }
    }
}
