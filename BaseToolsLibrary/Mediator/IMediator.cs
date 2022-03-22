using System;

namespace BaseToolsLibrary.Mediator
{
    /// <summary>
    ///     Mediator interface
    /// </summary>
    public interface IMediator
    {
        void Undo(IMediatorCommand command);

        IMediatorCommandResponse Execute(IMediatorCommand command);

        // to allow unit testing injection
        void RegisterHandler(IMediatorHandler handler, Type command);

    }
}
