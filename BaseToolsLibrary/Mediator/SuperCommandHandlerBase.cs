﻿using BaseToolsLibrary.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseToolsLibrary.Mediator
{
    /// <summary>
    ///     Base handler for the BaseSuperCommands implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SuperCommandHandlerBase<TCommand, TResponse> : BaseMediatorHandler<TCommand, TResponse>
        where TCommand : SuperCommandBase
        where TResponse : class, IMediatorCommandResponse
    {
        protected Lazy<IMediator> _mediator = new Lazy<IMediator>(() => DIContainer.GetImplementation<IMediator>());

        public SuperCommandHandlerBase()
        {
        }

        public override void Undo(IMediatorCommand command)
        {
            SuperCommandBase _command = this.castCommand(command);
            
            foreach (IMediatorCommand inner_command in _command.InnerCommands)
            {
                _mediator.Value.Undo(inner_command);
            }
            _command.InnerCommands.Clear();
        }
    }
}