using System;

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
        public abstract TResponse Execute(TCommand genericCommand);

        IMediatorCommandResponse IMediatorHandler.Execute(IMediatorCommand command)
        {
            return this.Execute(command as TCommand);
        }

        public abstract void Undo(TCommand genericCommand);

        void IMediatorHandler.Undo(IMediatorCommand command)
        {
            this.Undo(command as TCommand);
        }

        protected TCommand castCommand(IMediatorCommand command)
        {
            if (command is TCommand _command)
                return _command;
            Console.WriteLine($"ERROR : Wrong kind of command received for the MediatorHandler {this.GetType()}");
            throw new InvalidOperationException($"Wrong kind of command received for the MediatorHandler {this.GetType()}");
        }
    }
}
