using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseToolsLibrary.Mediator
{
    /// <summary>
    ///     Base class for every Command handler
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseMediatorHandler<TCommand, TResponse> : IMediatorHandler<TCommand, TResponse>
        where   TCommand : class, IMediatorCommand
        where   TResponse : class, IMediatorCommandResponse
    {
        public abstract TResponse Execute(IMediatorCommand command);

        IMediatorCommandResponse IMediatorHandler.Execute(IMediatorCommand command)
        {
            return this.Execute(command);
        }

        public abstract void Undo(IMediatorCommand command);

        protected TCommand castCommand(IMediatorCommand command)
        {
            if (command is TCommand _command)
                return _command;
            Console.WriteLine($"ERROR : Wrong kind of command recieved for the MediatorHandler {this.GetType()}");
            throw new InvalidOperationException($"Wrong kind of command recieved for the MediatorHandler {this.GetType()}");
        }
    }
}
