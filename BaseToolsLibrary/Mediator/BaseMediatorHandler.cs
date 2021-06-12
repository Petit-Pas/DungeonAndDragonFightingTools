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
    public abstract class BaseMediatorHandler<T> : IMediatorHandler<T>
        where T : class, IMediatorCommand
    {
        public abstract void Execute(IMediatorCommand command);

        public abstract void Undo(IMediatorCommand command);

        protected T cast_command(IMediatorCommand command)
        {
            if (command is T _command)
                return _command;
            Console.WriteLine($"ERROR : Wrong kind of command recieved for the MediatorHandler {this.GetType()}");
            throw new InvalidOperationException($"Wrong kind of command recieved for the MediatorHandler {this.GetType()}");
        }
    }
}
