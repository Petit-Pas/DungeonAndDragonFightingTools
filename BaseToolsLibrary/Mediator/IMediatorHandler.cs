﻿namespace BaseToolsLibrary.Mediator
{
    /// <summary>
    ///     Base interface for the handler of any IMediatorCommand
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMediatorHandler<TCommand, TReturn> : IMediatorHandler
        where TCommand : class, IMediatorCommand
        where TReturn : class, IMediatorCommandResponse
    {
        // declarations should be in the other interface, the non generic one
        new TReturn Execute(TCommand genericCommand);
    }

    public interface IMediatorHandler
    {
        // declarations should be in this interface, not the generic one

        IMediatorCommandResponse Execute(IMediatorCommand command);

        void Undo(IMediatorCommand genericCommand);
    }
}
