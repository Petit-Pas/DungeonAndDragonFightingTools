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
        public abstract TResponse Execute(IMediatorCommand genericCommand);

        IMediatorCommandResponse IMediatorHandler.Execute(IMediatorCommand genericCommand)
        {
            return this.Execute(genericCommand);
        }

        public abstract void Undo(IMediatorCommand genericCommand);

        protected TCommand castCommand(IMediatorCommand command)
        {
            if (command is TCommand _command)
                return _command;
            Console.WriteLine($"ERROR : Wrong kind of genericCommand received for the MediatorHandler {this.GetType()}");
            throw new InvalidOperationException($"Wrong kind of genericCommand received for the MediatorHandler {this.GetType()}");
        }
    }
}
